"use strict";

/**
 * extends an object target with 1..n object sources.
 * The attributes of the last attribute in the list of arguments will be
 * taken into account.
 * 
 * e.g.
 * var target  = { a:1, b:1};
 * var source1 = { b:2, c:2};
 * var source2 = { b:3, d:3};
 * var result = extend(target, source1, source2);
 * result = {a:1, b:3, c:2, d:3};
 * 
 * @param {Object} target
 * @param {Object} source1..n
 * @reult {Object}
 */
function extend() {
  var i, j, 
      target = {};
  for (i in arguments) {
    for (j in arguments[i]) {
      target[j] = arguments[i][j];
    }
  } 
  return target;
}

exports.extend = extend;

function pluck(obj, keys) {
  var result = {};
  keys.map(function(p){
    if (obj[p]) {
      result[p] = obj[p];
    }
  });
  return result;
}

exports.pluck = pluck;

