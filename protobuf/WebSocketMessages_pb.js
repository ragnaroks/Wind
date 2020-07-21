// source: WebSocketMessages.proto
/**
 * @fileoverview
 * @enhanceable
 * @suppress {messageConventions} JS Compiler reports an error if a variable or
 *     field starts with 'MSG_' and isn't a translatable message.
 * @public
 */
// GENERATED CODE -- DO NOT EDIT!

var jspb = require('google-protobuf');
var goog = jspb;
var global = Function('return this')();

goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf', null, global);
goog.exportSymbol('proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf', null, global);
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.repeatedFields_, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.repeatedFields_, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.repeatedFields_, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.displayName = 'proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf';
}



if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    name: jspb.Message.getFieldWithDefault(msg, 1, ""),
    description: jspb.Message.getFieldWithDefault(msg, 2, ""),
    type: jspb.Message.getFieldWithDefault(msg, 3, 0),
    absoluteexecutepath: jspb.Message.getFieldWithDefault(msg, 4, ""),
    absoluteworkdirectory: jspb.Message.getFieldWithDefault(msg, 5, ""),
    arguments: jspb.Message.getFieldWithDefault(msg, 6, ""),
    autostart: jspb.Message.getBooleanFieldWithDefault(msg, 7, false),
    autostartdelay: jspb.Message.getFieldWithDefault(msg, 8, 0),
    restartwhenexception: jspb.Message.getBooleanFieldWithDefault(msg, 9, false),
    priorityclass: jspb.Message.getFieldWithDefault(msg, 10, ""),
    processoraffinity: jspb.Message.getFieldWithDefault(msg, 11, ""),
    standardinputencoding: jspb.Message.getFieldWithDefault(msg, 12, ""),
    standardoutputencoding: jspb.Message.getFieldWithDefault(msg, 13, ""),
    standarderrorencoding: jspb.Message.getFieldWithDefault(msg, 14, ""),
    monitorperformanceusage: jspb.Message.getBooleanFieldWithDefault(msg, 15, false),
    monitornetworkusage: jspb.Message.getBooleanFieldWithDefault(msg, 16, false)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {string} */ (reader.readString());
      msg.setName(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setDescription(value);
      break;
    case 3:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setAbsoluteexecutepath(value);
      break;
    case 5:
      var value = /** @type {string} */ (reader.readString());
      msg.setAbsoluteworkdirectory(value);
      break;
    case 6:
      var value = /** @type {string} */ (reader.readString());
      msg.setArguments(value);
      break;
    case 7:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setAutostart(value);
      break;
    case 8:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setAutostartdelay(value);
      break;
    case 9:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setRestartwhenexception(value);
      break;
    case 10:
      var value = /** @type {string} */ (reader.readString());
      msg.setPriorityclass(value);
      break;
    case 11:
      var value = /** @type {string} */ (reader.readString());
      msg.setProcessoraffinity(value);
      break;
    case 12:
      var value = /** @type {string} */ (reader.readString());
      msg.setStandardinputencoding(value);
      break;
    case 13:
      var value = /** @type {string} */ (reader.readString());
      msg.setStandardoutputencoding(value);
      break;
    case 14:
      var value = /** @type {string} */ (reader.readString());
      msg.setStandarderrorencoding(value);
      break;
    case 15:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setMonitorperformanceusage(value);
      break;
    case 16:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setMonitornetworkusage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getName();
  if (f.length > 0) {
    writer.writeString(
      1,
      f
    );
  }
  f = message.getDescription();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      3,
      f
    );
  }
  f = message.getAbsoluteexecutepath();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
  f = message.getAbsoluteworkdirectory();
  if (f.length > 0) {
    writer.writeString(
      5,
      f
    );
  }
  f = message.getArguments();
  if (f.length > 0) {
    writer.writeString(
      6,
      f
    );
  }
  f = message.getAutostart();
  if (f) {
    writer.writeBool(
      7,
      f
    );
  }
  f = message.getAutostartdelay();
  if (f !== 0) {
    writer.writeInt32(
      8,
      f
    );
  }
  f = message.getRestartwhenexception();
  if (f) {
    writer.writeBool(
      9,
      f
    );
  }
  f = message.getPriorityclass();
  if (f.length > 0) {
    writer.writeString(
      10,
      f
    );
  }
  f = message.getProcessoraffinity();
  if (f.length > 0) {
    writer.writeString(
      11,
      f
    );
  }
  f = message.getStandardinputencoding();
  if (f.length > 0) {
    writer.writeString(
      12,
      f
    );
  }
  f = message.getStandardoutputencoding();
  if (f.length > 0) {
    writer.writeString(
      13,
      f
    );
  }
  f = message.getStandarderrorencoding();
  if (f.length > 0) {
    writer.writeString(
      14,
      f
    );
  }
  f = message.getMonitorperformanceusage();
  if (f) {
    writer.writeBool(
      15,
      f
    );
  }
  f = message.getMonitornetworkusage();
  if (f) {
    writer.writeBool(
      16,
      f
    );
  }
};


/**
 * optional string Name = 1;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getName = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 1, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setName = function(value) {
  return jspb.Message.setProto3StringField(this, 1, value);
};


/**
 * optional string Description = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getDescription = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setDescription = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional int32 Type = 3;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 3, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 3, value);
};


/**
 * optional string AbsoluteExecutePath = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getAbsoluteexecutepath = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setAbsoluteexecutepath = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};


/**
 * optional string AbsoluteWorkDirectory = 5;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getAbsoluteworkdirectory = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 5, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setAbsoluteworkdirectory = function(value) {
  return jspb.Message.setProto3StringField(this, 5, value);
};


/**
 * optional string Arguments = 6;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getArguments = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 6, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setArguments = function(value) {
  return jspb.Message.setProto3StringField(this, 6, value);
};


/**
 * optional bool AutoStart = 7;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getAutostart = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 7, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setAutostart = function(value) {
  return jspb.Message.setProto3BooleanField(this, 7, value);
};


/**
 * optional int32 AutoStartDelay = 8;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getAutostartdelay = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 8, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setAutostartdelay = function(value) {
  return jspb.Message.setProto3IntField(this, 8, value);
};


/**
 * optional bool RestartWhenException = 9;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getRestartwhenexception = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 9, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setRestartwhenexception = function(value) {
  return jspb.Message.setProto3BooleanField(this, 9, value);
};


/**
 * optional string PriorityClass = 10;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getPriorityclass = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 10, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setPriorityclass = function(value) {
  return jspb.Message.setProto3StringField(this, 10, value);
};


/**
 * optional string ProcessorAffinity = 11;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getProcessoraffinity = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 11, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setProcessoraffinity = function(value) {
  return jspb.Message.setProto3StringField(this, 11, value);
};


/**
 * optional string StandardInputEncoding = 12;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getStandardinputencoding = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 12, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setStandardinputencoding = function(value) {
  return jspb.Message.setProto3StringField(this, 12, value);
};


/**
 * optional string StandardOutputEncoding = 13;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getStandardoutputencoding = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 13, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setStandardoutputencoding = function(value) {
  return jspb.Message.setProto3StringField(this, 13, value);
};


/**
 * optional string StandardErrorEncoding = 14;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getStandarderrorencoding = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 14, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setStandarderrorencoding = function(value) {
  return jspb.Message.setProto3StringField(this, 14, value);
};


/**
 * optional bool MonitorPerformanceUsage = 15;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getMonitorperformanceusage = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 15, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setMonitorperformanceusage = function(value) {
  return jspb.Message.setProto3BooleanField(this, 15, value);
};


/**
 * optional bool MonitorNetworkUsage = 16;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.getMonitornetworkusage = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 16, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.prototype.setMonitornetworkusage = function(value) {
  return jspb.Message.setProto3BooleanField(this, 16, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    key: jspb.Message.getFieldWithDefault(msg, 1, ""),
    state: jspb.Message.getFieldWithDefault(msg, 2, 0),
    settingsfilepath: jspb.Message.getFieldWithDefault(msg, 3, ""),
    processid: jspb.Message.getFieldWithDefault(msg, 4, 0),
    processstarttime: jspb.Message.getFieldWithDefault(msg, 5, 0),
    processorcount: jspb.Message.getFieldWithDefault(msg, 6, 0),
    performancecountercpu: jspb.Message.getFloatingPointFieldWithDefault(msg, 7, 0.0),
    performancecounterram: jspb.Message.getFloatingPointFieldWithDefault(msg, 8, 0.0),
    networkcountertotalsent: jspb.Message.getFieldWithDefault(msg, 9, 0),
    networkcountertotalreceived: jspb.Message.getFieldWithDefault(msg, 10, 0),
    networkcountersendspeed: jspb.Message.getFieldWithDefault(msg, 11, 0),
    networkcounterreceivespeed: jspb.Message.getFieldWithDefault(msg, 12, 0),
    priorityclass: jspb.Message.getFieldWithDefault(msg, 13, 0),
    settingsprotobuf: (f = msg.getSettingsprotobuf()) && proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject(includeInstance, f),
    runningsettingsprotobuf: (f = msg.getRunningsettingsprotobuf()) && proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject(includeInstance, f)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {string} */ (reader.readString());
      msg.setKey(value);
      break;
    case 2:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setState(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setSettingsfilepath(value);
      break;
    case 4:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setProcessid(value);
      break;
    case 5:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setProcessstarttime(value);
      break;
    case 6:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setProcessorcount(value);
      break;
    case 7:
      var value = /** @type {number} */ (reader.readFloat());
      msg.setPerformancecountercpu(value);
      break;
    case 8:
      var value = /** @type {number} */ (reader.readFloat());
      msg.setPerformancecounterram(value);
      break;
    case 9:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcountertotalsent(value);
      break;
    case 10:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcountertotalreceived(value);
      break;
    case 11:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcountersendspeed(value);
      break;
    case 12:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcounterreceivespeed(value);
      break;
    case 13:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setPriorityclass(value);
      break;
    case 14:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader);
      msg.setSettingsprotobuf(value);
      break;
    case 15:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader);
      msg.setRunningsettingsprotobuf(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getKey();
  if (f.length > 0) {
    writer.writeString(
      1,
      f
    );
  }
  f = message.getState();
  if (f !== 0) {
    writer.writeInt32(
      2,
      f
    );
  }
  f = message.getSettingsfilepath();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
  f = message.getProcessid();
  if (f !== 0) {
    writer.writeInt32(
      4,
      f
    );
  }
  f = message.getProcessstarttime();
  if (f !== 0) {
    writer.writeInt64(
      5,
      f
    );
  }
  f = message.getProcessorcount();
  if (f !== 0) {
    writer.writeInt32(
      6,
      f
    );
  }
  f = message.getPerformancecountercpu();
  if (f !== 0.0) {
    writer.writeFloat(
      7,
      f
    );
  }
  f = message.getPerformancecounterram();
  if (f !== 0.0) {
    writer.writeFloat(
      8,
      f
    );
  }
  f = message.getNetworkcountertotalsent();
  if (f !== 0) {
    writer.writeInt64(
      9,
      f
    );
  }
  f = message.getNetworkcountertotalreceived();
  if (f !== 0) {
    writer.writeInt64(
      10,
      f
    );
  }
  f = message.getNetworkcountersendspeed();
  if (f !== 0) {
    writer.writeInt64(
      11,
      f
    );
  }
  f = message.getNetworkcounterreceivespeed();
  if (f !== 0) {
    writer.writeInt64(
      12,
      f
    );
  }
  f = message.getPriorityclass();
  if (f !== 0) {
    writer.writeInt32(
      13,
      f
    );
  }
  f = message.getSettingsprotobuf();
  if (f != null) {
    writer.writeMessage(
      14,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter
    );
  }
  f = message.getRunningsettingsprotobuf();
  if (f != null) {
    writer.writeMessage(
      15,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional string Key = 1;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getKey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 1, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setKey = function(value) {
  return jspb.Message.setProto3StringField(this, 1, value);
};


/**
 * optional int32 State = 2;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getState = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 2, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setState = function(value) {
  return jspb.Message.setProto3IntField(this, 2, value);
};


/**
 * optional string SettingsFilePath = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getSettingsfilepath = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setSettingsfilepath = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};


/**
 * optional int32 ProcessId = 4;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getProcessid = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 4, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setProcessid = function(value) {
  return jspb.Message.setProto3IntField(this, 4, value);
};


/**
 * optional int64 ProcessStartTime = 5;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getProcessstarttime = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 5, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setProcessstarttime = function(value) {
  return jspb.Message.setProto3IntField(this, 5, value);
};


/**
 * optional int32 ProcessorCount = 6;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getProcessorcount = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 6, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setProcessorcount = function(value) {
  return jspb.Message.setProto3IntField(this, 6, value);
};


/**
 * optional float PerformanceCounterCPU = 7;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getPerformancecountercpu = function() {
  return /** @type {number} */ (jspb.Message.getFloatingPointFieldWithDefault(this, 7, 0.0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setPerformancecountercpu = function(value) {
  return jspb.Message.setProto3FloatField(this, 7, value);
};


/**
 * optional float PerformanceCounterRAM = 8;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getPerformancecounterram = function() {
  return /** @type {number} */ (jspb.Message.getFloatingPointFieldWithDefault(this, 8, 0.0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setPerformancecounterram = function(value) {
  return jspb.Message.setProto3FloatField(this, 8, value);
};


/**
 * optional int64 NetworkCounterTotalSent = 9;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getNetworkcountertotalsent = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 9, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setNetworkcountertotalsent = function(value) {
  return jspb.Message.setProto3IntField(this, 9, value);
};


/**
 * optional int64 NetworkCounterTotalReceived = 10;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getNetworkcountertotalreceived = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 10, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setNetworkcountertotalreceived = function(value) {
  return jspb.Message.setProto3IntField(this, 10, value);
};


/**
 * optional int64 NetworkCounterSendSpeed = 11;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getNetworkcountersendspeed = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 11, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setNetworkcountersendspeed = function(value) {
  return jspb.Message.setProto3IntField(this, 11, value);
};


/**
 * optional int64 NetworkCounterReceiveSpeed = 12;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getNetworkcounterreceivespeed = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 12, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setNetworkcounterreceivespeed = function(value) {
  return jspb.Message.setProto3IntField(this, 12, value);
};


/**
 * optional int32 PriorityClass = 13;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getPriorityclass = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 13, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setPriorityclass = function(value) {
  return jspb.Message.setProto3IntField(this, 13, value);
};


/**
 * optional UnitSettingsProtobuf SettingsProtobuf = 14;
 * @return {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getSettingsprotobuf = function() {
  return /** @type{?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} */ (
    jspb.Message.getWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, 14));
};


/**
 * @param {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf|undefined} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setSettingsprotobuf = function(value) {
  return jspb.Message.setWrapperField(this, 14, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.clearSettingsprotobuf = function() {
  return this.setSettingsprotobuf(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.hasSettingsprotobuf = function() {
  return jspb.Message.getField(this, 14) != null;
};


/**
 * optional UnitSettingsProtobuf RunningSettingsProtobuf = 15;
 * @return {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.getRunningsettingsprotobuf = function() {
  return /** @type{?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} */ (
    jspb.Message.getWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, 15));
};


/**
 * @param {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf|undefined} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.setRunningsettingsprotobuf = function(value) {
  return jspb.Message.setWrapperField(this, 15, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.clearRunningsettingsprotobuf = function() {
  return this.setRunningsettingsprotobuf(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.prototype.hasRunningsettingsprotobuf = function() {
  return jspb.Message.getField(this, 15) != null;
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    absoluteexecutepath: jspb.Message.getFieldWithDefault(msg, 1, ""),
    absoluteworkdirectory: jspb.Message.getFieldWithDefault(msg, 2, ""),
    processid: jspb.Message.getFieldWithDefault(msg, 3, 0),
    processstarttime: jspb.Message.getFieldWithDefault(msg, 4, 0),
    processorcount: jspb.Message.getFieldWithDefault(msg, 5, 0),
    performancecountercpu: jspb.Message.getFloatingPointFieldWithDefault(msg, 6, 0.0),
    performancecounterram: jspb.Message.getFloatingPointFieldWithDefault(msg, 7, 0.0),
    networkcountertotalsent: jspb.Message.getFieldWithDefault(msg, 8, 0),
    networkcountertotalreceived: jspb.Message.getFieldWithDefault(msg, 9, 0),
    networkcountersendspeed: jspb.Message.getFieldWithDefault(msg, 10, 0),
    networkcounterreceivespeed: jspb.Message.getFieldWithDefault(msg, 11, 0),
    priorityclass: jspb.Message.getFieldWithDefault(msg, 12, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {string} */ (reader.readString());
      msg.setAbsoluteexecutepath(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setAbsoluteworkdirectory(value);
      break;
    case 3:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setProcessid(value);
      break;
    case 4:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setProcessstarttime(value);
      break;
    case 5:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setProcessorcount(value);
      break;
    case 6:
      var value = /** @type {number} */ (reader.readFloat());
      msg.setPerformancecountercpu(value);
      break;
    case 7:
      var value = /** @type {number} */ (reader.readFloat());
      msg.setPerformancecounterram(value);
      break;
    case 8:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcountertotalsent(value);
      break;
    case 9:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcountertotalreceived(value);
      break;
    case 10:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcountersendspeed(value);
      break;
    case 11:
      var value = /** @type {number} */ (reader.readInt64());
      msg.setNetworkcounterreceivespeed(value);
      break;
    case 12:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setPriorityclass(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getAbsoluteexecutepath();
  if (f.length > 0) {
    writer.writeString(
      1,
      f
    );
  }
  f = message.getAbsoluteworkdirectory();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getProcessid();
  if (f !== 0) {
    writer.writeInt32(
      3,
      f
    );
  }
  f = message.getProcessstarttime();
  if (f !== 0) {
    writer.writeInt64(
      4,
      f
    );
  }
  f = message.getProcessorcount();
  if (f !== 0) {
    writer.writeInt32(
      5,
      f
    );
  }
  f = message.getPerformancecountercpu();
  if (f !== 0.0) {
    writer.writeFloat(
      6,
      f
    );
  }
  f = message.getPerformancecounterram();
  if (f !== 0.0) {
    writer.writeFloat(
      7,
      f
    );
  }
  f = message.getNetworkcountertotalsent();
  if (f !== 0) {
    writer.writeInt64(
      8,
      f
    );
  }
  f = message.getNetworkcountertotalreceived();
  if (f !== 0) {
    writer.writeInt64(
      9,
      f
    );
  }
  f = message.getNetworkcountersendspeed();
  if (f !== 0) {
    writer.writeInt64(
      10,
      f
    );
  }
  f = message.getNetworkcounterreceivespeed();
  if (f !== 0) {
    writer.writeInt64(
      11,
      f
    );
  }
  f = message.getPriorityclass();
  if (f !== 0) {
    writer.writeInt32(
      12,
      f
    );
  }
};


/**
 * optional string AbsoluteExecutePath = 1;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getAbsoluteexecutepath = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 1, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setAbsoluteexecutepath = function(value) {
  return jspb.Message.setProto3StringField(this, 1, value);
};


/**
 * optional string AbsoluteWorkDirectory = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getAbsoluteworkdirectory = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setAbsoluteworkdirectory = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional int32 ProcessId = 3;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getProcessid = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 3, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setProcessid = function(value) {
  return jspb.Message.setProto3IntField(this, 3, value);
};


/**
 * optional int64 ProcessStartTime = 4;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getProcessstarttime = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 4, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setProcessstarttime = function(value) {
  return jspb.Message.setProto3IntField(this, 4, value);
};


/**
 * optional int32 ProcessorCount = 5;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getProcessorcount = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 5, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setProcessorcount = function(value) {
  return jspb.Message.setProto3IntField(this, 5, value);
};


/**
 * optional float PerformanceCounterCPU = 6;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getPerformancecountercpu = function() {
  return /** @type {number} */ (jspb.Message.getFloatingPointFieldWithDefault(this, 6, 0.0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setPerformancecountercpu = function(value) {
  return jspb.Message.setProto3FloatField(this, 6, value);
};


/**
 * optional float PerformanceCounterRAM = 7;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getPerformancecounterram = function() {
  return /** @type {number} */ (jspb.Message.getFloatingPointFieldWithDefault(this, 7, 0.0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setPerformancecounterram = function(value) {
  return jspb.Message.setProto3FloatField(this, 7, value);
};


/**
 * optional int64 NetworkCounterTotalSent = 8;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getNetworkcountertotalsent = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 8, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setNetworkcountertotalsent = function(value) {
  return jspb.Message.setProto3IntField(this, 8, value);
};


/**
 * optional int64 NetworkCounterTotalReceived = 9;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getNetworkcountertotalreceived = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 9, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setNetworkcountertotalreceived = function(value) {
  return jspb.Message.setProto3IntField(this, 9, value);
};


/**
 * optional int64 NetworkCounterSendSpeed = 10;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getNetworkcountersendspeed = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 10, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setNetworkcountersendspeed = function(value) {
  return jspb.Message.setProto3IntField(this, 10, value);
};


/**
 * optional int64 NetworkCounterReceiveSpeed = 11;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getNetworkcounterreceivespeed = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 11, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setNetworkcounterreceivespeed = function(value) {
  return jspb.Message.setProto3IntField(this, 11, value);
};


/**
 * optional int32 PriorityClass = 12;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.getPriorityclass = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 12, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.prototype.setPriorityclass = function(value) {
  return jspb.Message.setProto3IntField(this, 12, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.PacketTestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientKeepAliveProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    connectionid: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setConnectionid(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getConnectionid();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string ConnectionId = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.prototype.getConnectionid = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerAcceptConnectionProtobuf.prototype.setConnectionid = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    connectionid: jspb.Message.getFieldWithDefault(msg, 2, ""),
    controlkey: jspb.Message.getFieldWithDefault(msg, 3, ""),
    supportnotify: jspb.Message.getBooleanFieldWithDefault(msg, 4, false)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setConnectionid(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setControlkey(value);
      break;
    case 4:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setSupportnotify(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getConnectionid();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getControlkey();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
  f = message.getSupportnotify();
  if (f) {
    writer.writeBool(
      4,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string ConnectionId = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.getConnectionid = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.setConnectionid = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional string ControlKey = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.getControlkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.setControlkey = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};


/**
 * optional bool SupportNotify = 4;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.getSupportnotify = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 4, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ClientOfferControlKeyProtobuf.prototype.setSupportnotify = function(value) {
  return jspb.Message.setProto3BooleanField(this, 4, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    connectionid: jspb.Message.getFieldWithDefault(msg, 2, ""),
    valid: jspb.Message.getBooleanFieldWithDefault(msg, 3, false)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setConnectionid(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setValid(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getConnectionid();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getValid();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string ConnectionId = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.getConnectionid = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.setConnectionid = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Valid = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.getValid = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.ServerValidateConnectionProtobuf.prototype.setValid = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    line: jspb.Message.getFieldWithDefault(msg, 3, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setLine(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getLine();
  if (f !== 0) {
    writer.writeInt32(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional int32 Line = 3;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.getLine = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 3, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsRequestProtobuf.prototype.setLine = function(value) {
  return jspb.Message.setProto3IntField(this, 3, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    commandtype: jspb.Message.getFieldWithDefault(msg, 3, 0),
    commandline: jspb.Message.getFieldWithDefault(msg, 4, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setCommandtype(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setCommandline(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getCommandtype();
  if (f !== 0) {
    writer.writeInt32(
      3,
      f
    );
  }
  f = message.getCommandline();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional int32 CommandType = 3;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.getCommandtype = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 3, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.setCommandtype = function(value) {
  return jspb.Message.setProto3IntField(this, 3, value);
};


/**
 * optional string CommandLine = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.getCommandline = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineRequestProtobuf.prototype.setCommandline = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownRequestProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, ""),
    unitprotobuf: (f = msg.getUnitprotobuf()) && proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.toObject(includeInstance, f)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    case 5:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.deserializeBinaryFromReader);
      msg.setUnitprotobuf(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
  f = message.getUnitprotobuf();
  if (f != null) {
    writer.writeMessage(
      5,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};


/**
 * optional UnitProtobuf UnitProtobuf = 5;
 * @return {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.getUnitprotobuf = function() {
  return /** @type{?proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf} */ (
    jspb.Message.getWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf, 5));
};


/**
 * @param {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf|undefined} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.setUnitprotobuf = function(value) {
  return jspb.Message.setWrapperField(this, 5, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.clearUnitprotobuf = function() {
  return this.setUnitprotobuf(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusResponseProtobuf.prototype.hasUnitprotobuf = function() {
  return jspb.Message.getField(this, 5) != null;
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, ""),
    processid: jspb.Message.getFieldWithDefault(msg, 5, 0),
    unitrunningsettingsprotobuf: (f = msg.getUnitrunningsettingsprotobuf()) && proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject(includeInstance, f)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    case 5:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setProcessid(value);
      break;
    case 6:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader);
      msg.setUnitrunningsettingsprotobuf(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
  f = message.getProcessid();
  if (f !== 0) {
    writer.writeInt32(
      5,
      f
    );
  }
  f = message.getUnitrunningsettingsprotobuf();
  if (f != null) {
    writer.writeMessage(
      6,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};


/**
 * optional int32 ProcessId = 5;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.getProcessid = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 5, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.setProcessid = function(value) {
  return jspb.Message.setProto3IntField(this, 5, value);
};


/**
 * optional UnitSettingsProtobuf UnitRunningSettingsProtobuf = 6;
 * @return {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.getUnitrunningsettingsprotobuf = function() {
  return /** @type{?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} */ (
    jspb.Message.getWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, 6));
};


/**
 * @param {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf|undefined} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.setUnitrunningsettingsprotobuf = function(value) {
  return jspb.Message.setWrapperField(this, 6, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.clearUnitrunningsettingsprotobuf = function() {
  return this.setUnitrunningsettingsprotobuf(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartResponseProtobuf.prototype.hasUnitrunningsettingsprotobuf = function() {
  return jspb.Message.getField(this, 6) != null;
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, ""),
    unitsettingsprotobuf: (f = msg.getUnitsettingsprotobuf()) && proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject(includeInstance, f)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    case 5:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader);
      msg.setUnitsettingsprotobuf(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
  f = message.getUnitsettingsprotobuf();
  if (f != null) {
    writer.writeMessage(
      5,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};


/**
 * optional UnitSettingsProtobuf UnitSettingsProtobuf = 5;
 * @return {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.getUnitsettingsprotobuf = function() {
  return /** @type{?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} */ (
    jspb.Message.getWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, 5));
};


/**
 * @param {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf|undefined} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.setUnitsettingsprotobuf = function(value) {
  return jspb.Message.setWrapperField(this, 5, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.clearUnitsettingsprotobuf = function() {
  return this.setUnitsettingsprotobuf(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadResponseProtobuf.prototype.hasUnitsettingsprotobuf = function() {
  return jspb.Message.getField(this, 5) != null;
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};



/**
 * List of repeated fields within this message type.
 * @private {!Array<number>}
 * @const
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.repeatedFields_ = [6];



if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, ""),
    logfilepath: jspb.Message.getFieldWithDefault(msg, 5, ""),
    loglinearrayList: (f = jspb.Message.getRepeatedField(msg, 6)) == null ? undefined : f
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    case 5:
      var value = /** @type {string} */ (reader.readString());
      msg.setLogfilepath(value);
      break;
    case 6:
      var value = /** @type {string} */ (reader.readString());
      msg.addLoglinearray(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
  f = message.getLogfilepath();
  if (f.length > 0) {
    writer.writeString(
      5,
      f
    );
  }
  f = message.getLoglinearrayList();
  if (f.length > 0) {
    writer.writeRepeatedString(
      6,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};


/**
 * optional string LogFilePath = 5;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.getLogfilepath = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 5, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.setLogfilepath = function(value) {
  return jspb.Message.setProto3StringField(this, 5, value);
};


/**
 * repeated string LogLineArray = 6;
 * @return {!Array<string>}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.getLoglinearrayList = function() {
  return /** @type {!Array<string>} */ (jspb.Message.getRepeatedField(this, 6));
};


/**
 * @param {!Array<string>} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.setLoglinearrayList = function(value) {
  return jspb.Message.setField(this, 6, value || []);
};


/**
 * @param {string} value
 * @param {number=} opt_index
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.addLoglinearray = function(value, opt_index) {
  return jspb.Message.addToRepeatedField(this, 6, value, opt_index);
};


/**
 * Clears the list making it empty but non-null.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsResponseProtobuf.prototype.clearLoglinearrayList = function() {
  return this.setLoglinearrayList([]);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 3, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 4, ""),
    commandtype: jspb.Message.getFieldWithDefault(msg, 5, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 4:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    case 5:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setCommandtype(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      3,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      4,
      f
    );
  }
  f = message.getCommandtype();
  if (f !== 0) {
    writer.writeInt32(
      5,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional bool Executed = 3;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 3, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 3, value);
};


/**
 * optional string NoExecuteMessage = 4;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 4, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 4, value);
};


/**
 * optional int32 CommandType = 5;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.getCommandtype = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 5, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.CommandlineResponseProtobuf.prototype.setCommandtype = function(value) {
  return jspb.Message.setProto3IntField(this, 5, value);
};



/**
 * List of repeated fields within this message type.
 * @private {!Array<number>}
 * @const
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.repeatedFields_ = [5];



if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 2, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 3, ""),
    unitprotobufarraysize: jspb.Message.getFieldWithDefault(msg, 4, 0),
    unitprotobufarrayList: jspb.Message.toObjectList(msg.getUnitprotobufarrayList(),
    proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.toObject, includeInstance)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    case 4:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setUnitprotobufarraysize(value);
      break;
    case 5:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.deserializeBinaryFromReader);
      msg.addUnitprotobufarray(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      2,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
  f = message.getUnitprotobufarraysize();
  if (f !== 0) {
    writer.writeInt32(
      4,
      f
    );
  }
  f = message.getUnitprotobufarrayList();
  if (f.length > 0) {
    writer.writeRepeatedMessage(
      5,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional bool Executed = 2;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 2, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 2, value);
};


/**
 * optional string NoExecuteMessage = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};


/**
 * optional int32 UnitProtobufArraySize = 4;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.getUnitprotobufarraysize = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 4, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.setUnitprotobufarraysize = function(value) {
  return jspb.Message.setProto3IntField(this, 4, value);
};


/**
 * repeated UnitProtobuf UnitProtobufArray = 5;
 * @return {!Array<!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf>}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.getUnitprotobufarrayList = function() {
  return /** @type{!Array<!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf>} */ (
    jspb.Message.getRepeatedWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf, 5));
};


/**
 * @param {!Array<!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf>} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.setUnitprotobufarrayList = function(value) {
  return jspb.Message.setRepeatedWrapperField(this, 5, value);
};


/**
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf=} opt_value
 * @param {number=} opt_index
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.addUnitprotobufarray = function(opt_value, opt_index) {
  return jspb.Message.addToRepeatedWrapperField(this, 5, opt_value, proto.wind.Entities.Protobuf.WebSocketMessages.UnitProtobuf, opt_index);
};


/**
 * Clears the list making it empty but non-null.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StatusAllResponseProtobuf.prototype.clearUnitprotobufarrayList = function() {
  return this.setUnitprotobufarrayList([]);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 2, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 3, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      2,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional bool Executed = 2;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 2, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 2, value);
};


/**
 * optional string NoExecuteMessage = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartAllResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 2, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 3, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      2,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional bool Executed = 2;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 2, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 2, value);
};


/**
 * optional string NoExecuteMessage = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopAllResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 2, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 3, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      2,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional bool Executed = 2;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 2, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 2, value);
};


/**
 * optional string NoExecuteMessage = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RestartAllResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};



/**
 * List of repeated fields within this message type.
 * @private {!Array<number>}
 * @const
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.repeatedFields_ = [5];



if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 2, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 3, ""),
    unitsettingsprotobufarraysize: jspb.Message.getFieldWithDefault(msg, 4, 0),
    unitsettingsprotobufarrayList: jspb.Message.toObjectList(msg.getUnitsettingsprotobufarrayList(),
    proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject, includeInstance)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    case 4:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setUnitsettingsprotobufarraysize(value);
      break;
    case 5:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader);
      msg.addUnitsettingsprotobufarray(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      2,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
  f = message.getUnitsettingsprotobufarraysize();
  if (f !== 0) {
    writer.writeInt32(
      4,
      f
    );
  }
  f = message.getUnitsettingsprotobufarrayList();
  if (f.length > 0) {
    writer.writeRepeatedMessage(
      5,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional bool Executed = 2;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 2, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 2, value);
};


/**
 * optional string NoExecuteMessage = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};


/**
 * optional int32 UnitSettingsProtobufArraySize = 4;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.getUnitsettingsprotobufarraysize = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 4, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.setUnitsettingsprotobufarraysize = function(value) {
  return jspb.Message.setProto3IntField(this, 4, value);
};


/**
 * repeated UnitSettingsProtobuf UnitSettingsProtobufArray = 5;
 * @return {!Array<!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf>}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.getUnitsettingsprotobufarrayList = function() {
  return /** @type{!Array<!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf>} */ (
    jspb.Message.getRepeatedWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, 5));
};


/**
 * @param {!Array<!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf>} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.setUnitsettingsprotobufarrayList = function(value) {
  return jspb.Message.setRepeatedWrapperField(this, 5, value);
};


/**
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf=} opt_value
 * @param {number=} opt_index
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.addUnitsettingsprotobufarray = function(opt_value, opt_index) {
  return jspb.Message.addToRepeatedWrapperField(this, 5, opt_value, proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, opt_index);
};


/**
 * Clears the list making it empty but non-null.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadAllResponseProtobuf.prototype.clearUnitsettingsprotobufarrayList = function() {
  return this.setUnitsettingsprotobufarrayList([]);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 2, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 3, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      2,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional bool Executed = 2;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 2, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 2, value);
};


/**
 * optional string NoExecuteMessage = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveAllResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    major: jspb.Message.getFieldWithDefault(msg, 2, 0),
    minor: jspb.Message.getFieldWithDefault(msg, 3, 0),
    build: jspb.Message.getFieldWithDefault(msg, 4, 0),
    revision: jspb.Message.getFieldWithDefault(msg, 5, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setMajor(value);
      break;
    case 3:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setMinor(value);
      break;
    case 4:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setBuild(value);
      break;
    case 5:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setRevision(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getMajor();
  if (f !== 0) {
    writer.writeInt32(
      2,
      f
    );
  }
  f = message.getMinor();
  if (f !== 0) {
    writer.writeInt32(
      3,
      f
    );
  }
  f = message.getBuild();
  if (f !== 0) {
    writer.writeInt32(
      4,
      f
    );
  }
  f = message.getRevision();
  if (f !== 0) {
    writer.writeInt32(
      5,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional int32 Major = 2;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.getMajor = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 2, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.setMajor = function(value) {
  return jspb.Message.setProto3IntField(this, 2, value);
};


/**
 * optional int32 Minor = 3;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.getMinor = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 3, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.setMinor = function(value) {
  return jspb.Message.setProto3IntField(this, 3, value);
};


/**
 * optional int32 Build = 4;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.getBuild = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 4, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.setBuild = function(value) {
  return jspb.Message.setProto3IntField(this, 4, value);
};


/**
 * optional int32 Revision = 5;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.getRevision = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 5, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonVersionResponseProtobuf.prototype.setRevision = function(value) {
  return jspb.Message.setProto3IntField(this, 5, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    daemonprotobuf: (f = msg.getDaemonprotobuf()) && proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.toObject(includeInstance, f)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.deserializeBinaryFromReader);
      msg.setDaemonprotobuf(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getDaemonprotobuf();
  if (f != null) {
    writer.writeMessage(
      2,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional DaemonProtobuf DaemonProtobuf = 2;
 * @return {?proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.getDaemonprotobuf = function() {
  return /** @type{?proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf} */ (
    jspb.Message.getWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf, 2));
};


/**
 * @param {?proto.wind.Entities.Protobuf.WebSocketMessages.DaemonProtobuf|undefined} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.setDaemonprotobuf = function(value) {
  return jspb.Message.setWrapperField(this, 2, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.clearDaemonprotobuf = function() {
  return this.setDaemonprotobuf(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonStatusResponseProtobuf.prototype.hasDaemonprotobuf = function() {
  return jspb.Message.getField(this, 2) != null;
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    executed: jspb.Message.getBooleanFieldWithDefault(msg, 2, false),
    noexecutemessage: jspb.Message.getFieldWithDefault(msg, 3, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setExecuted(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setNoexecutemessage(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getExecuted();
  if (f) {
    writer.writeBool(
      2,
      f
    );
  }
  f = message.getNoexecutemessage();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional bool Executed = 2;
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.getExecuted = function() {
  return /** @type {boolean} */ (jspb.Message.getBooleanFieldWithDefault(this, 2, false));
};


/**
 * @param {boolean} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.setExecuted = function(value) {
  return jspb.Message.setProto3BooleanField(this, 2, value);
};


/**
 * optional string NoExecuteMessage = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.getNoexecutemessage = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.DaemonShutdownResponseProtobuf.prototype.setNoexecutemessage = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StartNotifyProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.StopNotifyProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    unitsettingsprotobuf: (f = msg.getUnitsettingsprotobuf()) && proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.toObject(includeInstance, f)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = new proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf;
      reader.readMessage(value,proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.deserializeBinaryFromReader);
      msg.setUnitsettingsprotobuf(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getUnitsettingsprotobuf();
  if (f != null) {
    writer.writeMessage(
      3,
      f,
      proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf.serializeBinaryToWriter
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional UnitSettingsProtobuf UnitSettingsProtobuf = 3;
 * @return {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.getUnitsettingsprotobuf = function() {
  return /** @type{?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf} */ (
    jspb.Message.getWrapperField(this, proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf, 3));
};


/**
 * @param {?proto.wind.Entities.Protobuf.WebSocketMessages.UnitSettingsProtobuf|undefined} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf} returns this
*/
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.setUnitsettingsprotobuf = function(value) {
  return jspb.Message.setWrapperField(this, 3, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.clearUnitsettingsprotobuf = function() {
  return this.setUnitsettingsprotobuf(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LoadNotifyProtobuf.prototype.hasUnitsettingsprotobuf = function() {
  return jspb.Message.getField(this, 3) != null;
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.RemoveNotifyProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.toObject = function(opt_includeInstance) {
  return proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.toObject = function(includeInstance, msg) {
  var f, obj = {
    type: jspb.Message.getFieldWithDefault(msg, 1, 0),
    unitkey: jspb.Message.getFieldWithDefault(msg, 2, ""),
    logline: jspb.Message.getFieldWithDefault(msg, 3, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf;
  return proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setType(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setUnitkey(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setLogline(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getType();
  if (f !== 0) {
    writer.writeInt32(
      1,
      f
    );
  }
  f = message.getUnitkey();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getLogline();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
};


/**
 * optional int32 Type = 1;
 * @return {number}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.getType = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/**
 * @param {number} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.setType = function(value) {
  return jspb.Message.setProto3IntField(this, 1, value);
};


/**
 * optional string UnitKey = 2;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.getUnitkey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.setUnitkey = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional string LogLine = 3;
 * @return {string}
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.getLogline = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf} returns this
 */
proto.wind.Entities.Protobuf.WebSocketMessages.LogsNotifyProtobuf.prototype.setLogline = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};


goog.object.extend(exports, proto.wind.Entities.Protobuf.WebSocketMessages);
