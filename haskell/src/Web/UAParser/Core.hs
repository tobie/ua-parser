{-# LANGUAGE DeriveDataTypeable        #-}
{-# LANGUAGE NoMonomorphismRestriction #-}
{-# LANGUAGE OverloadedStrings         #-}
{-# LANGUAGE RecordWildCards           #-}

module Web.UAParser.Core
    ( UAConfig (..)
    , loadConfig
    , parseUA
    , UAResult (..)
    , uarVersion
    , parseOS
    , OSResult (..)
    , osrVersion

    -- * Utilities
    , decodeFile'
    ) where


-------------------------------------------------------------------------------
import           Control.Applicative
import           Control.Monad
import           Data.Aeson
import           Data.ByteString.Char8 (ByteString)
import qualified Data.ByteString.Char8 as B
import           Data.Default
import           Data.Generics
import           Data.Maybe
import           Data.Text             (Text)
import qualified Data.Text             as T
import qualified Data.Text.Encoding    as T
import           Data.Yaml
import           Text.Regex.PCRE.Light
-------------------------------------------------------------------------------



test :: [ByteString]
test =
  ["SonyEricssonK750i/R1L Browser/SEMC-Browser/4.2 Profile/MIDP-2.0 Configuration/CLDC-1.1"
  , "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-GB; rv:1.8.1.18) Gecko/20081029 Firefox/2.0.0.18"
  , "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_5_5; en-us) AppleWebKit/525.26.2 (KHTML, like Gecko) Version/3.2 Safari/525.26.12'"
  , "Mozilla/4.0 (compatible; MSIE 6.0; Windows XP 5.1) Lobo/0.98.4"
  , "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; )'"
  , "Opera/9.80 (Windows NT 5.1; U; cs) Presto/2.2.15 Version/10.00'"
  , "boxee (alpha/Darwin 8.7.1 i386 - 0.9.11.5591)'"
  , "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; CSM-NEWUSER; GTB6; byond_4.0; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 1.1.4322; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.1)'"
  , "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)'"
  ]


                                ---------------
                                -- UA Parser --
                                ---------------


-------------------------------------------------------------------------------
-- | Parse a given User-Agent string
parseUA
    :: UAConfig
    -- ^ Loaded parser configuration data
    -> ByteString
    -- ^ User-Agent string to be parsed
    -> Maybe UAResult
parseUA UAConfig{..} bs = foldr mplus Nothing $ map go uaParsers
    where
      go UAParser{..} = mkRes . map T.decodeUtf8 =<< match uaRegex bs []
        where
          mkRes [] = Nothing
          mkRes [_,f,v1,v2,v3] = Just $ UAResult (repF f) (repV1 v1) (Just v2) (Just v3)
          mkRes [_,f,v1,v2] = Just $ UAResult (repF f) (repV1 v1) (Just v2) Nothing
          mkRes [_,f,v1] = Just $ UAResult (repF f) (repV1 v1) Nothing Nothing
          mkRes [_, f] = Just $ UAResult (repF f) Nothing Nothing Nothing
          mkRes x = Nothing
          -- error $  "Unsupported match in parseUA" ++ show x

          repV1 x = uaV1Rep `mplus` Just x
          repF x = maybe x id uaFamRep



-------------------------------------------------------------------------------
-- | Results datatype for the parsed User-Agent
data UAResult = UAResult {
      uarFamily :: Text
    , uarV1     :: Maybe Text
    , uarV2     :: Maybe Text
    , uarV3     :: Maybe Text
    } deriving (Show, Read, Eq, Typeable, Data)


-------------------------------------------------------------------------------
-- | Construct a browser version-string from 'UAResult'
uarVersion :: UAResult -> Text
uarVersion UAResult{..} =
    T.intercalate "." . catMaybes . takeWhile isJust $ [uarV1, uarV2, uarV3]


-------------------------------------------------------------------------------
instance Default UAResult where
    def = UAResult "" Nothing Nothing Nothing



                                ---------------
                                -- OS Parser --
                                ---------------


-------------------------------------------------------------------------------
-- | Parse OS from given User-Agent string
parseOS
    :: UAConfig
    -- ^ Loaded parser data
    -> ByteString
    -- ^ User-Agent string to be parsed
    -> Maybe OSResult
parseOS UAConfig{..} bs = foldr mplus Nothing $ map go osParsers
    where
      go OSParser{..} = mkRes . map T.decodeUtf8 =<< match osRegex bs []
          where
          mkRes [] = Nothing
          mkRes [_,f,v1,v2,v3, v4] = Just $
            OSResult (repF f) (Just v1) (Just v2) (Just v3) (Just v4)
          mkRes [_,f,v1,v2,v3] = Just $ OSResult (repF f) (Just v1) (Just v2) (Just v3) Nothing
          mkRes [_,f,v1,v2] = Just $ OSResult (repF f) (Just v1) (Just v2) Nothing Nothing
          mkRes [_,f,v1] = Just $ OSResult (repF f) (Just v1) Nothing Nothing Nothing
          mkRes [_, f] = Just $ OSResult (repF f) Nothing Nothing Nothing Nothing
          mkRes x = Nothing
          -- error $  "Unsupported match in parseOS" ++ show x

          repF x = maybe x id osFamRep


-------------------------------------------------------------------------------
-- | Result type for 'parseOS'
data OSResult = OSResult {
      osrFamily :: Text
    , osrV1     :: Maybe Text
    , osrV2     :: Maybe Text
    , osrV3     :: Maybe Text
    , osrV4     :: Maybe Text
    } deriving (Show,Read,Eq,Typeable,Data)

instance Default OSResult where
    def = OSResult "" Nothing Nothing Nothing Nothing


-------------------------------------------------------------------------------
-- | Construct a version string from 'OSResult'
osrVersion :: OSResult -> Text
osrVersion OSResult{..} =
    T.intercalate "." . catMaybes . takeWhile isJust $ [osrV1, osrV2, osrV3, osrV4]

                              -------------------
                              -- Parser Config --
                              -------------------


-------------------------------------------------------------------------------
loadConfig :: FilePath -> IO UAConfig
loadConfig fp = either error id  `fmap` decodeFile' fp


-------------------------------------------------------------------------------
decodeFile' :: FromJSON a => FilePath -> IO (Either String a)
decodeFile' fp = decodeEither `fmap` B.readFile fp


-------------------------------------------------------------------------------
-- | User-Agent string parser data
data UAConfig = UAConfig {
      uaParsers  :: [UAParser]
    , osParsers  :: [OSParser]
    , devParsers :: [DevParser]
    } deriving (Eq,Show)


-------------------------------------------------------------------------------
data UAParser = UAParser {
      uaRegex  :: Regex
    , uaFamRep :: Maybe Text
    , uaV1Rep  :: Maybe Text
    } deriving (Eq,Show)


-------------------------------------------------------------------------------
data OSParser = OSParser {
      osRegex  :: Regex
    , osFamRep :: Maybe Text
    , osRep1   :: Maybe Text
    , osRep2   :: Maybe Text
    } deriving (Eq,Show)


data DevParser = DevParser {
      devRegex :: Regex
    , devRep   :: Maybe Text
    } deriving (Eq,Show)


-------------------------------------------------------------------------------
parseRegex :: Object -> Parser Regex
parseRegex v = flip compile [] `liftM` (v .: "regex")


-------------------------------------------------------------------------------
instance FromJSON UAConfig where
    parseJSON (Object v) =
      UAConfig
        <$> v .: "user_agent_parsers"
        <*> v .: "os_parsers"
        <*> v .: "device_parsers"
    parseJSON _ = error "Object expected when parsing JSON"


-------------------------------------------------------------------------------
instance FromJSON UAParser where
    parseJSON (Object v) =
      UAParser <$> parseRegex v
               <*> (v .:? "family_replacement" <|> return Nothing)
               <*> (v .:? "v1_replacement" <|> return Nothing)
    parseJSON _ = error "Object expected when parsing JSON"


-------------------------------------------------------------------------------
instance FromJSON OSParser where
    parseJSON (Object v) =
      OSParser <$> parseRegex v
               <*> (v .:? "os_replacement" <|> return Nothing)
               <*> (v .:? "os_v1_replacement" <|> return Nothing)
               <*> (v .:? "os_v2_replacement" <|> return Nothing)
    parseJSON _ = error "Object expected when parsing JSON"


-------------------------------------------------------------------------------
instance FromJSON DevParser where
    parseJSON (Object v) =
      DevParser <$> parseRegex v
                <*> v .:? "device_replacement"
    parseJSON _ = error "Object expected when parsing JSON"

