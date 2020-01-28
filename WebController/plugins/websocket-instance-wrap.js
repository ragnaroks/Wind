import Protocol from '@/assets/protocol_pb.common';

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
    //内部方法,实例预配置
    const instanceSetup=function(hostname,url,controlKey,vuex){
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
    //内部方法,实例进行链接
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
            //clear unit status
            const payload4={hostname:wrap.hostname};
            vuexInstance.commit('clear_unitsStatusArray_In_DaemonItem',payload4);
            //clear daemon meta
            const payload5={hostname:wrap.hostname,daemonVersion:null,daemonWorkDirectory:null,daemonHostCpuCores:null,daemonHostMemorySize:null,daemonProcessId:null};
            vuexInstance.commit('set_daemonMetaValues_In_DaemonItem',payload5);
            vueInstance.$Notice.info({title:wrap.hostname,desc:'connection has been close'});
        };
        instance.onmessage=function(event){
            if(!event.data || !(event.data instanceof ArrayBuffer)){return;}
            dataArray.push(event.data);
            const payload={hostname:wrap.hostname,length:event.data.byteLength};
            vuexInstance.commit('increase_receivedLength_In_WebsocketWrap_In_DaemonItem',payload);
            if(!Protocol.WebSocketPacketTest){throw new Error('protobuf.WebSocketPacketTest undefind');}
            const packetTest=Protocol.WebSocketPacketTest.deserializeBinary(event.data);
            const packetType=packetTest.getType();
            window.ed=event.data;
            switch(packetType){
                //服务端响应客户端链接事件,并回复给客户端
                case 2001:instanceReceiveWebSocketServerResponseAfterOnOpen(event.data);break;
                //服务端回复客户端ControlKey验证结果
                case 2002:instanceReceiveWebSocketServerResponseValidateControlKey(event.data);break;
                //服务端回复客户端Daemon元数据
                case 2003:instanceReceiveWebSocketServerResponseFetchDaemonMeta(event.data);break;
                //服务端回复客户端Daemon状态
                case 2004:instanceReceiveWebSocketServerResponseFetchDaemonStatus(event.data);break;
                //服务端回复客户端所有单元状态
                case 2005:instanceReceiveWebSocketServerResponseFetchUnitsStatus(event.data);break;
                //服务端回复客户端指定单元状态
                case 2006:instanceReceiveWebSocketServerResponseFetchUnitStatus(event.data);break;
                //服务端回复客户端重载所有单元配置
                case 2007:instanceReceiveWebSocketServerResponseReloadUnitsSettings(event.data);break;
                //服务端回复客户端重载指定单元配置
                case 2008:instanceReceiveWebSocketServerResponseReloadUnitSettings(event.data);break;
                //服务端回复客户端启动所有单元
                case 2009:instanceReceiveWebSocketServerResponseStartUnits(event.data);break;
                //服务端回复客户端启动指定单元
                case 2010:instanceReceiveWebSocketServerResponseStartUnit(event.data);break;
                //服务端回复客户端停止所有单元
                case 2011:instanceReceiveWebSocketServerResponseStopUnits(event.data);break;
                //服务端回复客户端停止指定单元
                case 2012:instanceReceiveWebSocketServerResponseStopUnit(event.data);break;
                //服务端通知所有客户端指定单元被重载
                case 2013:instanceReceiveWebSocketServerNotifyClientsThatUnitSettingsReload(event.data);break;
                //服务端通知所有客户端指定单元已启动
                case 2014:instanceReceiveWebSocketServerNotifyClientsThatUnitStarted(event.data);break;
                //服务端通知所有客户端指定单元已停止
                case 2015:instanceReceiveWebSocketServerNotifyClientsThatUnitStopped(event.data);break;
                //服务端通知所有客户端指定单元启动失败
                case 2016:instanceReceiveWebSocketServerNotifyClientsThatUnitStartFailed(event.data);break;
                //服务端通知所有客户端指定单元停止失败
                case 2017:instanceReceiveWebSocketServerNotifyClientsThatUnitStopFailed(event.data);break;
                //服务端通知所有客户端指定单元网络数据
                case 2018:instanceReceiveWebSocketServerResponseFetchUnitStatusNetworkCounter(event.data);break;
                //其它
                default:console.log('unknown packetType',packetType);break;
            }
        };
    };
    //内部方法,实例断开
    const instanceClose=function(){
        instance.close();
        instance=null;
    };
    //内部方法,实例发送二进制消息
    const instanceSendMessage=function(message){
        if(!message || !(message instanceof ArrayBuffer)){return;}
        instance.send(message);
        const payload={hostname:wrap.hostname,length:message.byteLength};
        vuexInstance.commit('increase_sentLength_In_WebsocketWrap_In_DaemonItem',payload);
    };
    //内部方法,实例收到消息 2001=>服务端响应客户端链接事件,并回复给客户端
    const instanceReceiveWebSocketServerResponseAfterOnOpen=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseAfterOnOpen.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseAfterOnOpen',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const connectionId=packet.getClientconnectionguid();
        const payload={hostname:wrap.hostname,connectionId:connectionId};
        vuexInstance.commit('set_connectionId_In_WebsocketWrap_In_DaemonItem',payload);
        //向服务器发起验证
        instanceSendWebSocketClientRequestValidateControlKey();
    };
    //内部方法,实例发送消息 1002=>客户端向服务端请求验证ControlKey
    const instanceSendWebSocketClientRequestValidateControlKey=function(){
        const packet=new Protocol.WebSocketClientRequestValidateControlKey();
        packet.setType(1002);
        packet.setClientconnectionguid(wrap.connectionId);
        packet.setControlkey(wrap.controlKey);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2002=>服务端回复客户端ControlKey验证结果
    const instanceReceiveWebSocketServerResponseValidateControlKey=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseValidateControlKey.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseValidateControlKey',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const connectionValid=packet.getValidation();
        const payload={hostname:wrap.hostname,connectionValid:connectionValid};
        vuexInstance.commit('set_connectionValid_In_WebsocketWrap_In_DaemonItem',payload);
        if(connectionValid){
            vueInstance.$Notice.success({title:wrap.hostname,desc:'connection has validated'});
        }
        //请求Daemon元数据
        instanceSendWebSocketClientRequestFetchDaemonMeta();
    };
    //内部方法,实例发送消息 1003=>客户端向服务端请求Daemon元数据
    const instanceSendWebSocketClientRequestFetchDaemonMeta=function(){
        if(!wrap.connected || !wrap.connectionValid){return;}
        const packet=new Protocol.WebSocketClientRequestFetchDaemonMeta();
        packet.setType(1003);
        packet.setClientconnectionguid(wrap.connectionId);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2003=>服务端回复客户端Daemon元数据
    const instanceReceiveWebSocketServerResponseFetchDaemonMeta=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseFetchDaemonMeta.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseFetchDaemonMeta',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const packetDaemonMeta=packet.getDaemonmeta();
        const payload={
            hostname:wrap.hostname,
            daemonVersion:packetDaemonMeta.getVersion(),
            daemonWorkDirectory:packetDaemonMeta.getWorkdirectory(),
            daemonHostCpuCores:packetDaemonMeta.getHostcpucores(),
            daemonHostMemorySize:packetDaemonMeta.getHostmemorysize(),
            daemonProcessId:packetDaemonMeta.getProcessid()
        };
        vuexInstance.commit('set_daemonMetaValues_In_DaemonItem',payload);
        //客户端向服务端请求所有单元状态
        instanceSendWebSocketClientRequestFetchUnitsStatus();
    };
    //内部方法,实例发送消息 1004=>客户端向服务端请求Daemon状态
    const instanceSendWebSocketClientRequestFetchDaemonStatus=function(){
        if(!wrap.connected || !wrap.connectionValid){return;}
        const packet=new Protocol.WebSocketClientRequestFetchDaemonStatus();
        packet.setType(1004);
        packet.setClientconnectionguid(wrap.connectionId);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2004=>服务端回复客户端Daemon状态
    const instanceReceiveWebSocketServerResponseFetchDaemonStatus=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseFetchDaemonStatus.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseFetchDaemonStatus',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const packetDaemonStatus=packet.getDaemonstatus();
        const payload={
            hostname:wrap.hostname,
            daemonProcessTimePercentage:packetDaemonStatus.getProcesstimepercentage(),
            daemonProcessWorkingSetSize:packetDaemonStatus.getProcessworkingsetsize(),
            daemonUnitSettingsCount:packetDaemonStatus.getUnitsettingscount(),
            daemonUnitProcessCount:packetDaemonStatus.getUnitprocesscount(),
            daemonNetworkTotalSent:packetDaemonStatus.getNetworktotalsent(),
            daemonNetworkTotalReceived:packetDaemonStatus.getNetworktotalreceived()
        };
        vuexInstance.commit('set_daemonStatusValues_In_DaemonItem',payload);
    };
    //内部方法,实例发送消息 1005=>客户端向服务端请求所有单元状态
    const instanceSendWebSocketClientRequestFetchUnitsStatus=function(){
        if(!wrap.connected || !wrap.connectionValid){return;}
        const packet=new Protocol.WebSocketClientRequestFetchUnitsStatus();
        packet.setType(1005);
        packet.setClientconnectionguid(wrap.connectionId);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2005=>服务端回复客户端所有单元状态
    const instanceReceiveWebSocketServerResponseFetchUnitsStatus=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseFetchUnitsStatus.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseFetchUnitsStatus',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const unitsStatusArrayPacket=packet.getUnitstatusList();
        if(!unitsStatusArrayPacket || unitsStatusArrayPacket.length<1){return;}
        const unitsStatusArray=[];
        for(let i1=0;i1<unitsStatusArrayPacket.length;i1++){
            const unitStatusItem={unitName:unitsStatusArrayPacket[i1].getUnitname()};
            const unitStatusItemUnitSettings=unitsStatusArrayPacket[i1].getUnitsettings();
            unitStatusItem.unitSettings={
                name:unitStatusItemUnitSettings.getName(),
                description:unitStatusItemUnitSettings.getDescription(),
                executeAbsolutePath:unitStatusItemUnitSettings.getExecuteabsolutepath(),
                workAbsoluteDirectory:unitStatusItemUnitSettings.getWorkabsolutedirectory(),
                executeParams:unitStatusItemUnitSettings.getExecuteparams(),
                autoStart:unitStatusItemUnitSettings.getAutostart(),
                autoStartDelay:unitStatusItemUnitSettings.getAutostartdelay(),
                daemonProcess:unitStatusItemUnitSettings.getDaemonprocess(),
                haveChildProcesses:unitStatusItemUnitSettings.getHavechildprocesses(),
                fetchNetworkUsage:unitStatusItemUnitSettings.getFetchnetworkusage()
            };
            const unitStatusItemUnitProcess=unitsStatusArrayPacket[i1].getUnitprocess();
            unitStatusItem.unitProcess={
                name:unitStatusItemUnitProcess.getName(),
                state:unitStatusItemUnitProcess.getState(),
                processId:unitStatusItemUnitProcess.getProcessid()
            };
            const unitStatusItemUnitNetworkCounter=unitsStatusArrayPacket[i1].getUnitnetworkcounter();
            unitStatusItem.unitNetworkCounter={
                unitName:unitStatusItemUnitNetworkCounter.getName(),
                totalSent:unitStatusItemUnitNetworkCounter.getTotalsent(),
                totalReceived:unitStatusItemUnitNetworkCounter.getTotalreceived(),
                sendSpeed:unitStatusItemUnitNetworkCounter.getSendspeed(),
                receiveSpeed:unitStatusItemUnitNetworkCounter.getReceivespeed()
            };
            unitsStatusArray[i1]=unitStatusItem;
        }
        const payload={hostname:wrap.hostname,unitsStatusArray:unitsStatusArray};
        vuexInstance.commit('set_unitsStatusArray_In_DaemonItem',payload);
        vueInstance.$Notice.success({title:wrap.hostname,desc:'fetched all units status'});
    };
    //内部方法,实例发送消息 1006=>客户端向服务端请求指定单元状态
    const instanceSendWebSocketClientRequestFetchUnitStatus=function(methodArgsObject){
        if(!wrap.connected || !wrap.connectionValid){return;}
        if(!methodArgsObject || !methodArgsObject.unitName){return;}
        const packet=new Protocol.WebSocketClientRequestFetchUnitStatus();
        packet.setType(1006);
        packet.setClientconnectionguid(wrap.connectionId);
        packet.setUnitname(methodArgsObject.unitName);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2006=>服务端回复客户端指定单元状态
    const instanceReceiveWebSocketServerResponseFetchUnitStatus=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseFetchUnitStatus.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseFetchUnitStatus',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const unitStatusItemPacket=packet.getUnitstatus();
        if(!unitStatusItemPacket){return;}
        const unitStatusItem={unitName:unitStatusItemPacket.getUnitname()};
        const unitStatusItemUnitSettings=unitStatusItemPacket.getUnitsettings();
        unitStatusItem.unitSettings={
            name:unitStatusItemUnitSettings.getName(),
            description:unitStatusItemUnitSettings.getDescription(),
            executeAbsolutePath:unitStatusItemUnitSettings.getExecuteabsolutepath(),
            workAbsoluteDirectory:unitStatusItemUnitSettings.getWorkabsolutedirectory(),
            executeParams:unitStatusItemUnitSettings.getExecuteparams(),
            autoStart:unitStatusItemUnitSettings.getAutostart(),
            autoStartDelay:unitStatusItemUnitSettings.getAutostartdelay(),
            daemonProcess:unitStatusItemUnitSettings.getDaemonprocess(),
            haveChildProcesses:unitStatusItemUnitSettings.getHavechildprocesses(),
            fetchNetworkUsage:unitStatusItemUnitSettings.getFetchnetworkusage()
        };
        const unitStatusItemUnitProcess=unitStatusItemPacket.getUnitprocess();
        unitStatusItem.unitProcess={
            name:unitStatusItemUnitProcess.getName(),
            state:unitStatusItemUnitProcess.getState(),
            processId:unitStatusItemUnitProcess.getProcessid()
        };
        const unitStatusItemUnitNetworkCounter=unitStatusItemPacket.getUnitnetworkcounter();
        unitStatusItem.unitNetworkCounter={
            unitName:unitStatusItemUnitNetworkCounter.getName(),
            totalSent:unitStatusItemUnitNetworkCounter.getTotalsent(),
            totalReceived:unitStatusItemUnitNetworkCounter.getTotalreceived(),
            sendSpeed:unitStatusItemUnitNetworkCounter.getSendspeed(),
            receiveSpeed:unitStatusItemUnitNetworkCounter.getReceivespeed()
        };
        const payload={hostname:wrap.hostname,unitStatusItem:unitStatusItem};
        vuexInstance.commit('set_unitStatusItem_In_unitsStatusArray_In_DaemonItem',payload);
        vueInstance.$Notice.success({title:wrap.hostname,desc:'fetched ['+unitStatusItem.unitName+'] unit status'});
    };
    //内部方法,实例发送消息 1007=>客户端向服务端请求重载所有单元配置
    const instanceSendWebSocketClientRequestReloadUnitsSettings=function(){
        if(!wrap.connected || !wrap.connectionValid){return;}
        const packet=new Protocol.WebSocketClientRequestReloadUnitsSettings();
        packet.setType(1007);
        packet.setClientconnectionguid(wrap.connectionId);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2007=>服务端回复客户端重载所有单元配置
    const instanceReceiveWebSocketServerResponseReloadUnitsSettings=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseReloadUnitsSettings.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseReloadUnitsSettings',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        if(!packet.getExecuted()){return;}
        vueInstance.$Notice.info({title:wrap.hostname,desc:'reload all units settings'});
    };
    //内部方法,实例发送消息 1008=>客户端向服务端请求重载指定单元配置
    const instanceSendWebSocketClientRequestReloadUnitSettings=function(methodArgsObject){
        if(!wrap.connected || !wrap.connectionValid){return;}
        if(!methodArgsObject || !methodArgsObject.unitName){return;}
        const packet=new Protocol.WebSocketClientRequestReloadUnitSettings();
        packet.setType(1008);
        packet.setClientconnectionguid(wrap.connectionId);
        packet.setRestartifupdate(true);
        packet.setUnitname(methodArgsObject.unitName);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2008=>服务端回复客户端重载指定单元配置
    const instanceReceiveWebSocketServerResponseReloadUnitSettings=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseReloadUnitSettings.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseReloadUnitSettings',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        if(!packet.getExecuted()){return;}
        vueInstance.$Notice.info({title:wrap.hostname,desc:'reload ['+packet.getUnitname()+'] unit settings'});
    };
    //内部方法,实例发送消息 1009=>客户端向服务端请求启动所有单元
    const instanceSendWebSocketClientRequestStartUnits=function(){
        if(!wrap.connected || !wrap.connectionValid){return;}
        const packet=new Protocol.WebSocketClientRequestStartUnits();
        packet.setType(1009);
        packet.setClientconnectionguid(wrap.connectionId);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2009=>服务端回复客户端启动所有单元
    const instanceReceiveWebSocketServerResponseStartUnits=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseStartUnits.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseStartUnits',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        if(!packet.getExecuted()){return;}
        vueInstance.$Notice.info({title:wrap.hostname,desc:'start all units'});
    };
    //内部方法,实例发送消息 1010=>客户端向服务端请求启动指定单元
    const instanceSendWebSocketClientRequestStartUnit=function(methodArgsObject){
        if(!wrap.connected || !wrap.connectionValid){return;}
        if(!methodArgsObject || !methodArgsObject.unitName){return;}
        const packet=new Protocol.WebSocketClientRequestStartUnit();
        packet.setType(1010);
        packet.setClientconnectionguid(wrap.connectionId);
        packet.setUnitname(methodArgsObject.unitName);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2010=>服务端回复客户端启动指定单元
    const instanceReceiveWebSocketServerResponseStartUnit=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseStartUnit.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseStartUnit',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        if(!packet.getExecuted()){return;}
        vueInstance.$Notice.info({title:wrap.hostname,desc:'start ['+packet.getUnitname()+'] unit'});
    };
    //内部方法,实例发送消息 1011=>客户端向服务端请求停止所有单元
    const instanceSendWebSocketClientRequestStopUnits=function(){
        if(!wrap.connected || !wrap.connectionValid){return;}
        const packet=new Protocol.WebSocketClientRequestStopUnits();
        packet.setType(1011);
        packet.setClientconnectionguid(wrap.connectionId);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2011=>服务端回复客户端停止所有单元
    const instanceReceiveWebSocketServerResponseStopUnits=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseStopUnits.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseStopUnits',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        if(!packet.getExecuted()){return;}
        vueInstance.$Notice.info({title:wrap.hostname,desc:'stop all units'});
    };
    //内部方法,实例发送消息 1012=>客户端向服务端请求停止指定单元
    const instanceSendWebSocketClientRequestStopUnit=function(methodArgsObject){
        if(!wrap.connected || !wrap.connectionValid){return;}
        if(!methodArgsObject || !methodArgsObject.unitName){return;}
        const packet=new Protocol.WebSocketClientRequestStopUnit();
        packet.setType(1012);
        packet.setClientconnectionguid(wrap.connectionId);
        packet.setUnitname(methodArgsObject.unitName);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2012=>服务端回复客户端停止指定单元
    const instanceReceiveWebSocketServerResponseStopUnit=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseStopUnit.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseStopUnit',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        if(!packet.getExecuted()){return;}
        vueInstance.$Notice.info({title:wrap.hostname,desc:'stop ['+packet.getUnitname()+'] unit'});
    };
    //内部方法,实例收到消息 2013=>服务端通知所有客户端指定单元被重载
    const instanceReceiveWebSocketServerNotifyClientsThatUnitSettingsReload=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerNotifyClientsThatUnitSettingsReload.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerNotifyClientsThatUnitSettingsReload',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const unitSettingsPacket=packet.getUnitsettings();
        if(!unitSettingsPacket){return;}
        const unitSettings={
            name:unitSettingsPacket.getName(),
            description:unitSettingsPacket.getDescription(),
            executeAbsolutePath:unitSettingsPacket.getExecuteabsolutepath(),
            workAbsoluteDirectory:unitSettingsPacket.getWorkabsolutedirectory(),
            executeParams:unitSettingsPacket.getExecuteparams(),
            autoStart:unitSettingsPacket.getAutostart(),
            autoStartDelay:unitSettingsPacket.getAutostartdelay(),
            daemonProcess:unitSettingsPacket.getDaemonprocess(),
            haveChildProcesses:unitSettingsPacket.getHavechildprocesses()
        };
        const payload={hostname:wrap.hostname,unitSettings:unitSettings};
        vuexInstance.commit('set_unitSettings_In_unitStatusItem_In_unitsStatusArray_In_DaemonItem',payload);
        vueInstance.$Notice.success({title:wrap.hostname,desc:'reload ['+packet.getUnitname()+'] unit success'});
    };
    //内部方法,实例收到消息 2014=>服务端通知所有客户端指定单元已启动
    const instanceReceiveWebSocketServerNotifyClientsThatUnitStarted=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerNotifyClientsThatUnitStarted.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerNotifyClientsThatUnitStarted',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const unitProcessPacket=packet.getUnitprocess();
        if(!unitProcessPacket){return;}
        const unitProcess={
            name:unitProcessPacket.getName(),
            state:unitProcessPacket.getState(),
            processId:unitProcessPacket.getProcessid()
        };
        const payload={hostname:wrap.hostname,unitProcess:unitProcess};
        vuexInstance.commit('set_unitProcess_In_unitStatusItem_In_unitsStatusArray_In_DaemonItem',payload);
        vueInstance.$Notice.success({title:wrap.hostname,desc:'start ['+packet.getUnitname()+'] unit success'});
    };
    //内部方法,实例收到消息 2015=>服务端通知所有客户端指定单元已停止
    const instanceReceiveWebSocketServerNotifyClientsThatUnitStopped=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerNotifyClientsThatUnitStopped.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerNotifyClientsThatUnitStopped',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const unitProcess={
            name:packet.getUnitname(),
            state:1,
            processId:0
        };
        const payload={hostname:wrap.hostname,unitProcess:unitProcess};
        vuexInstance.commit('set_unitProcess_In_unitStatusItem_In_unitsStatusArray_In_DaemonItem',payload);
        const unitNetworkCounter={
            name:packet.getUnitname(),
            receiveSpeed:0,
            sendSpeed:0,
            totalReceived:0,
            totalSent:0
        };
        const payload2={hostname:wrap.hostname,unitNetworkCounter:unitNetworkCounter};
        vuexInstance.commit('set_unitNetworkCounter_In_unitStatusItem_In_unitsStatusArray_In_DaemonItem',payload2);
        vueInstance.$Notice.success({title:wrap.hostname,desc:'stop ['+packet.getUnitname()+'] unit success'});
    };
    //内部方法,实例收到消息 2016=>服务端通知所有客户端指定单元启动失败
    const instanceReceiveWebSocketServerNotifyClientsThatUnitStartFailed=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerNotifyClientsThatUnitStartFailed.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerNotifyClientsThatUnitStartFailed',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        vueInstance.$Notice.error({title:wrap.hostname,desc:'start ['+packet.getUnitname()+'] unit failed'});
    };
    //内部方法,实例收到消息 2017=>服务端通知所有客户端指定单元停止失败
    const instanceReceiveWebSocketServerNotifyClientsThatUnitStopFailed=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerNotifyClientsThatUnitStopFailed.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerNotifyClientsThatUnitStopFailed',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        vueInstance.$Notice.error({title:wrap.hostname,desc:'stop ['+packet.getUnitname()+'] unit failed'});
    };
    //内部方法,实例发送消息 1018=>客户端向服务端请求指定单元网络数据
    const instanceSendWebSocketClientRequestFetchUnitStatusNetworkCounter=function(methodArgsObject){
        if(!wrap.connected || !wrap.connectionValid){return;}
        if(!methodArgsObject || !methodArgsObject.unitName){return;}
        const packet=new Protocol.WebSocketClientRequestFetchUnitStatusNetworkCounter();
        packet.setType(1018);
        packet.setClientconnectionguid(wrap.connectionId);
        packet.setUnitname(methodArgsObject.unitName);
        instanceSendMessage(packet.serializeBinary().buffer);
    };
    //内部方法,实例收到消息 2018=>服务端回复客户端指定单元网络数据
    const instanceReceiveWebSocketServerResponseFetchUnitStatusNetworkCounter=function(data){
        let packet=null;
        try{
            packet=Protocol.WebSocketServerResponseFetchUnitStatusNetworkCounter.deserializeBinary(data);
        }catch(error){
            console.error('instanceReceiveWebSocketServerResponseFetchUnitStatusNetworkCounter',error);
            vueInstance.$Notice.error({title:wrap.hostname,desc:'connection has an error,see details in devtool(F12)'});
            return;
        }
        const unitNetworkCounterPacket=packet.getUnitnetworkcounter();
        if(!unitNetworkCounterPacket){return;}
        const unitNetworkCounter={
            name:unitNetworkCounterPacket.getName(),
            totalSent:unitNetworkCounterPacket.getTotalsent(),
            totalReceived:unitNetworkCounterPacket.getTotalreceived(),
            sendSpeed:unitNetworkCounterPacket.getSendspeed(),
            receiveSpeed:unitNetworkCounterPacket.getReceivespeed()
        };
        const payload={hostname:wrap.hostname,unitNetworkCounter:unitNetworkCounter};
        vuexInstance.commit('set_unitNetworkCounter_In_unitStatusItem_In_unitsStatusArray_In_DaemonItem',payload);
    };
    //公开方法
    this.Setup=function(hostname,url,controlKey,vuex){
        try{
            instanceSetup(hostname,url,controlKey,vuex);
        }catch(error){
            throw error;
        }
    };
    this.Connect=function(){instanceConnect();};
    this.SendMessage=function(message){instanceSendMessage(message);};
    this.Close=function(){instanceClose();};
    //额外公开方法
    this.Invoke=function(methodName,methodArgsObject){
        switch(methodName){
            //客户端向服务端请求Daemon元数据
            case 'instanceSendWebSocketClientRequestFetchDaemonMeta':instanceSendWebSocketClientRequestFetchDaemonMeta();break;
            //客户端向服务端请求Daemon状态
            case 'instanceSendWebSocketClientRequestFetchDaemonStatus':instanceSendWebSocketClientRequestFetchDaemonStatus();break;
            //客户端向服务端请求所有单元状态
            case 'instanceSendWebSocketClientRequestFetchUnitsStatus':instanceSendWebSocketClientRequestFetchUnitsStatus();break;
            //客户端向服务端请求指定单元状态
            case 'instanceSendWebSocketClientRequestFetchUnitStatus':instanceSendWebSocketClientRequestFetchUnitStatus(methodArgsObject);break;
            //客户端向服务端请求重载所有单元配置
            case 'instanceSendWebSocketClientRequestReloadUnitsSettings':instanceSendWebSocketClientRequestReloadUnitsSettings();break;
            //客户端向服务端请求重载指定单元配置
            case 'instanceSendWebSocketClientRequestReloadUnitSettings':instanceSendWebSocketClientRequestReloadUnitSettings(methodArgsObject);break;
            //客户端向服务端请求启动所有单元
            case 'instanceSendWebSocketClientRequestStartUnits':instanceSendWebSocketClientRequestStartUnits();break;
            //客户端向服务端请求启动指定单元
            case 'instanceSendWebSocketClientRequestStartUnit':instanceSendWebSocketClientRequestStartUnit(methodArgsObject);break;
            //客户端向服务端请求停止所有单元
            case 'instanceSendWebSocketClientRequestStopUnits':instanceSendWebSocketClientRequestStopUnits();break;
            //客户端向服务端请求停止指定单元
            case 'instanceSendWebSocketClientRequestStopUnit':instanceSendWebSocketClientRequestStopUnit(methodArgsObject);break;
            //客户端向服务器请求获取指定单元的网络数据
            case 'instanceSendWebSocketClientRequestFetchUnitStatusNetworkCounter':instanceSendWebSocketClientRequestFetchUnitStatusNetworkCounter(methodArgsObject);break;
            //其它
            default:console.log('unknown Invoke method',methodName,methodArgsObject);break;
        }
    };
};
