import Protocol from '@/assets/protocol.common';

window.WebSocketInstanceWrap=function WebSocketInstanceWrap(){
    const wrap=this;
    //公开属性
    this.url=null;
    this.controlKey=null;
    this.vueInstance=null;
    this.hostname=null;
    this.receivedLength=0;
    this.sentLength=0;
    this.connected=false;
    this.connectionId=null;
    this.connectionValid=false;
    //内部属性
    let instance=null;
    let vuexInstance=null;
    let vueInstance=null;
    const dataArray=[];
    //内部方法
    const setup=function(hostname,url,controlKey,vuex){
        //setup 方法最终在vuex内调用,所以此处不需要commit
        if(!hostname){throw new Error('<hostname> must be not empty');}
        wrap.hostname=hostname;
        //setup 方法最终在vuex内调用,所以此处不需要commit
        if(!url){throw new Error('<url> must be not empty');}
        wrap.url=url;
        //setup 方法最终在vuex内调用,所以此处不需要commit
        if(!controlKey){throw new Error('<controlKey> must be not empty');}
        wrap.controlKey=controlKey;
        //内部属性
        if(!vuex){throw new Error('<vuexInstance> must be not empty');}
        vuexInstance=vuex;
        vueInstance=vuex._vm;
    };
    const instanceConnect=function(){
        try{
            //instance 是内部变量,不需要commit
            instance=new WebSocket(wrap.url);
        }catch(error){
            throw error;
        }
        instance.binaryType='arraybuffer';
        instance.onopen=function(event){
            //console.log('instanceOnOpen',event,wrap);
            //wrap.connected=true;
            const payload={hostname:wrap.hostname,connected:true};
            vuexInstance.commit('set_connected_In_WebsocketWrap_In_DaemonItem',payload);
            vueInstance.$Notice.info({title:wrap.hostname,desc:'connection has been open'});
        };
        instance.onerror=function(event){
            //console.error('instanceOnError',event,wrap);
            console.error('connection error',event);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection on error,see details in devtool(F12)',duration:0});
        };
        instance.onclose=function(event){
            //console.log('instanceOnClose',event,wrap);
            //wrap.connectionId=null;
            const payload1={hostname:wrap.hostname,connectionId:null};
            vuexInstance.commit('set_connectionId_In_WebsocketWrap_In_DaemonItem',payload1);
            //wrap.connected=false;
            const payload2={hostname:wrap.hostname,connected:false};
            vuexInstance.commit('set_connected_In_WebsocketWrap_In_DaemonItem',payload2);
            //wrap.connectionValid=false;
            const payload3={hostname:wrap.hostname,connectionValid:false};
            vuexInstance.commit('set_connectionValid_In_WebsocketWrap_In_DaemonItem',payload3);
            const payload4={hostname:wrap.hostname};
            //clear unit status
            vuexInstance.commit('clear_unitStatusArray_In_DaemonItem',payload4);
            vueInstance.$Notice.info({title:wrap.hostname,desc:'connection has been close'});
        };
        instance.onmessage=function(event){
            //console.log('instanceOnMessage',event,wrap);
            if(!event.data || !(event.data instanceof ArrayBuffer)){return;}
            dataArray.push(event.data);
            //wrap.receivedLength+=event.data.byteLength;
            const payload={hostname:wrap.hostname,length:event.data.byteLength};
            vuexInstance.commit('increase_receivedLength_In_WebsocketWrap_In_DaemonItem',payload);
            if(!Protocol.WebSocketPacketTest){throw new Error('protobuf.WebSocketPacketTest undefind');}
            const packetTest=Protocol.WebSocketPacketTest.deserializeBinary(event.data);
            const packetType=packetTest.getType();
            switch(packetType){
                //服务端响应客户端链接事件,并回复给客户端
                case 2001:instanceReceiveWebSocketServerResponseAfterOnOpen(Protocol.WebSocketServerResponseAfterOnOpen.deserializeBinary(event.data));break;
                case 2002:instanceReceiveWebSocketServerResponseValidateControlKey(Protocol.WebSocketServerResponseValidateControlKey.deserializeBinary(event.data));break;
                default:console.log('unknown packetType',packetType);break;
            }
        };
    };
    const instanceClose=function(){
        instance.close();
        instance=null;
    };
    const instanceSendMessage=function(message){
        if(!message || !(message instanceof ArrayBuffer)){return;}
        instance.send(message);
        const payload={hostname:wrap.hostname,length:message.byteLength};
        vuexInstance.commit('increase_sentLength_In_WebsocketWrap_In_DaemonItem',payload);
    };
    const instanceReceiveWebSocketServerResponseAfterOnOpen=function(packet){
        //wrap.connectionId=packet.getClientconnectionguid();
        const connectionId=packet.getClientconnectionguid();
        const payload={hostname:wrap.hostname,connectionId:connectionId};
        vuexInstance.commit('set_connectionId_In_WebsocketWrap_In_DaemonItem',payload);
        //向服务器发起验证
        instanceSendWebSocketClientRequestValidateControlKey();
    };
    const instanceSendWebSocketClientRequestValidateControlKey=function(){
        const packet=new Protocol.WebSocketClientRequestValidateControlKey();
        packet.setType(1002);
        packet.setClientconnectionguid(wrap.connectionId);
        packet.setControlkey(wrap.controlKey);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    const instanceReceiveWebSocketServerResponseValidateControlKey=function(packet){
        //console.log('instanceReceiveWebSocketServerResponseValidateControlKey',packet);
        //wrap.connectionValid=packet.getValidation();
        const connectionValid=packet.getValidation();
        const payload={hostname:wrap.hostname,connectionValid:connectionValid};
        vuexInstance.commit('set_connectionValid_In_WebsocketWrap_In_DaemonItem',payload);
        if(connectionValid){
            vueInstance.$Notice.success({title:wrap.hostname,desc:'connection has validated'});
        }
    };
    //公开方法
    this.Setup=function(hostname,url,controlKey,vuex){
        try{
            setup(hostname,url,controlKey,vuex);
        }catch(error){
            throw error;
        }
    };
    this.Connect=function(){
        instanceConnect();
    };
    this.SendMessage=function(message){
        instanceSendMessage(message);
    };
    this.Close=function(){
        instanceClose();
    };
};
