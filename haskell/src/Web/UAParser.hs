{-# LANGUAGE NoMonomorphismRestriction #-}
{-# LANGUAGE OverloadedStrings         #-}
{-# LANGUAGE RecordWildCards           #-}

module Web.UAParser
    (
    -- * Readying parser
      UAConfig
    , loadUAParser
    -- * Parsing browser (user agent)
    , parseUA
    , UAResult (..)
    , uarVersion
    -- * Parsing OS
    , parseOS
    , OSResult (..)
    , osrVersion
    ) where


-------------------------------------------------------------------------------
import           Control.Applicative
import           Control.Monad
import           Data.Aeson
import           Data.ByteString.Char8 (ByteString)
import qualified Data.ByteString.Char8 as B
import           Data.Text             (Text)
import qualified Data.Text             as T
import qualified Data.Text.Encoding    as T
import           Data.Yaml
import           System.FilePath.Posix
import           Text.Regex.PCRE.Light
-------------------------------------------------------------------------------
import           Paths_ua_parser
import           Web.UAParser.Core
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- | Load a user agent string parser state, ready to be used with one
-- of the parsing functions.
--
-- This function will load the YAML parser definitions stored in
-- package's cabal 'getDataDir'.
loadUAParser :: IO UAConfig
loadUAParser = do
  dir <- getDataDir
  loadConfig $ dir </> "regexes.yaml"
