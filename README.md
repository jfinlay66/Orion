# Orion
Developer scenario

Math endpoint - accepts a string expression and returns the calculated decimal result
NOTES:
 - accepts a string with numbers and currently supported operators (+-*/)
 - does not support parentheses, but operator precedence is taken into account (*/ are applied before +-)
 - unary + and - operators are supported
 - requirements stated to support up to 5 values.  this supports more, but an interface could constrain that.

Example debug call:
http://localhost:51358/orion/math?expression=1%2B2%2A3%2F4-5
