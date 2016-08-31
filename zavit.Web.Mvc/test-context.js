var context = require.context('./Specs', true, /-spec\.js$/);
context.keys().forEach(context);