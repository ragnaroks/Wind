export const state= () => ({
    currentDaemonHostname:null,
    currentDaemonItem:null,//应该只读
    daemonArray:[
        {
            hostname:'localhost',
            websocketAddress:'ws://localhost:25565',
            websocketControlKey:'https://github.com/ragnaroks/Wind2',
            websocketWrap:null,
            unitStatusArray:[]
        },{
            hostname:'127.0.0.1',
            websocketAddress:'ws://127.0.0.1:25565',
            websocketControlKey:'https://github.com/ragnaroks/Wind2',
            websocketWrap:null,
            unitStatusArray:[]
        },{
            hostname:'::1',
            websocketAddress:'ws://[::1]:25565',
            websocketControlKey:'https://github.com/ragnaroks/Wind2',
            websocketWrap:null,
            unitStatusArray:[]
        }
    ]
});

export const mutations = {
    set_currentDaemonHostname:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        state.currentDaemonHostname=payload.hostname;
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.currentDaemonItem=state.daemonArray[i1];
            break;
        }
        if(!state.currentDaemonItem.websocketWrap){
            state.currentDaemonItem.websocketWrap=new window.WebSocketInstanceWrap();
            state.currentDaemonItem.websocketWrap.Setup(state.currentDaemonItem.hostname,state.currentDaemonItem.websocketAddress,state.currentDaemonItem.websocketControlKey,this);
        }
    },
    set_connected_In_WebsocketWrap_In_DaemonItem:function(state,payload){
        if(!payload.hostname || payload.connected===undefined){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].websocketWrap.connected=payload.connected;
            break;
        }
    },
    set_connectionId_In_WebsocketWrap_In_DaemonItem:function(state,payload){
        if(!payload.hostname || payload.connectionId===undefined){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].websocketWrap.connectionId=payload.connectionId;
            break;
        }
    },
    set_connectionValid_In_WebsocketWrap_In_DaemonItem:function(state,payload){
        if(!payload.hostname || payload.connectionValid===undefined){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].websocketWrap.connectionValid=payload.connectionValid;
            break;
        }
    },
    increase_receivedLength_In_WebsocketWrap_In_DaemonItem:function(state,payload){
        if(!payload.hostname || payload.length===undefined){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].websocketWrap.receivedLength+=payload.length;
            break;
        }
    },
    increase_sentLength_In_WebsocketWrap_In_DaemonItem:function(state,payload){
        if(!payload.hostname || payload.length===undefined){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].websocketWrap.sentLength+=payload.length;
            break;
        }
    },
    clear_unitStatusArray_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].unitStatusArray=[];
            break;
        }
    }
    /*set_CurrentDaemon:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.currentDaemonItem=state.daemonArray[i1];
            break;
        }
    },*/
    /*set_WebSocketWrap_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].websocketWrap=payload.websocketWrap;
            break;
        }
    }*/
    /*
    //set WebSocketItem
    set_WebSocketItem_Instance:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].instance=payload.instance;
        }
    },
    set_WebSocketItem_Connected:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].connected=payload.connected;
        }
    },
    set_WebSocketItem_ConnectionId:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].connectionId=payload.connectionId;
        }
    },
    set_WebSocketItem_ConnectionValid:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].connectionValid=payload.connectionValid;
        }
    },
    set_WebSocketItem_RecvivedText:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].recvivedText=payload.recvivedText;
        }
    },
    set_WebSocketItem_RecvivedLength:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].recvivedLength=payload.recvivedLength;
        }
    },
    set_WebSocketItem_SentText:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].sentText=payload.sentText;
        }
    },
    set_WebSocketItem_SentLength:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].sentLength=payload.sentLength;
        }
    },
    append_WebSocketItem_RecvivedText:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname || !payload.recvivedText){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].recvivedText+=payload.recvivedText;
        }
    },
    append_WebSocketItem_SentText:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname || !payload.sentText){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].sentText+=payload.sentText;
        }
    },
    increase_WebSocketItem_RecvivedLength:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname || payload.recvivedLength<1){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].recvivedLength+=payload.recvivedLength;
        }
    },
    increase_WebSocketItem_SentLength:function(state,payload){
        if(state.webSocketArray.length<1){return;}
        if(!payload.hostname || payload.sentLength<1){return;}
        for(let i1=0;i1<state.webSocketArray.length;i1++){
            if(state.webSocketArray[i1].hostname!==payload.hostname){continue;}
            state.webSocketArray[i1].sentLength+=payload.sentLength;
        }
    },
    //set daemonUnitStatusArray
    set_Unit_To_DaemonUnitStatusArray:function(state,payload){
        if(!state.daemonUnitStatusArray[payload.hostname]){
            Vue.set(state.daemonUnitStatusArray,payload.hostname,[]);
        }
        if(state.daemonUnitStatusArray[payload.hostname].length<1){
            state.daemonUnitStatusArray[payload.hostname].push(payload.unitStatusItem);
            return;
        }
        for(let i1=0;i1<state.daemonUnitStatusArray[payload.hostname].length;i1++){
            if(state.daemonUnitStatusArray[payload.hostname][i1].UnitName!==payload.UnitName){continue;}
            Vue.set(state.daemonUnitStatusArray[payload.hostname],payload.UnitName,payload.unitStatusItem);
            return;
        }
        state.daemonUnitStatusArray[payload.hostname].push(payload.unitStatusItem);
    },
    remove_Unit_From_DaemonUnitStatusArray:function(state,payload){
        if(!state.daemonUnitStatusArray[payload.hostname]){return;}
        if(state.daemonUnitStatusArray[payload.hostname].length<1){return;}
        let index=-1;
        for(let i1=0;i1<state.daemonUnitStatusArray[payload.hostname].length;i1++){
            if(state.daemonUnitStatusArray[payload.hostname][i1].UnitName!==payload.UnitName){continue;}
            index=i1;
        }
        if(index<0){return;}
        state.daemonUnitStatusArray[payload.hostname].splice(index,1);
    },
    clear_Units_In_DaemonUnitStatusArray:function(state,payload){
        if(!state.daemonUnitStatusArray[payload.hostname]){return;}
        Vue.set(state.daemonUnitStatusArray,payload.hostname,[]);
    },
    set_Unit_UnitProcess_State_In_DaemonUnitStatusArray:function(state,payload){
        if(!state.daemonUnitStatusArray[payload.hostname]){return;}
        if(state.daemonUnitStatusArray[payload.hostname].length<1){return;}
        for(let i1=0;i1<state.daemonUnitStatusArray[payload.hostname].length;i1++){
            if(state.daemonUnitStatusArray[payload.hostname][i1].UnitName!==payload.UnitName){continue;}
            if(!state.daemonUnitStatusArray[payload.hostname][i1].UnitProcess){
                Vue.set(state.daemonUnitStatusArray[payload.hostname][i1],'UnitProcess',{Name:payload.UnitName,State:1,ProcessId:0});
            }
            if(payload.State===3){
                Vue.set(state.daemonUnitStatusArray[payload.hostname][i1].UnitProcess,'State',3);
                if(payload.ProcessId!==undefined){
                    Vue.set(state.daemonUnitStatusArray[payload.hostname][i1].UnitProcess,'ProcessId',payload.ProcessId);
                }
            }else{
                Vue.set(state.daemonUnitStatusArray[payload.hostname][i1].UnitProcess,'State',1);
                Vue.set(state.daemonUnitStatusArray[payload.hostname][i1].UnitProcess,'ProcessId',payload.ProcessId);
            }
        }
    },
    set_Unit_UnitSettings_In_DaemonUnitStatusArray:function(state,payload){
        console.log('set_Unit_UnitSettings_In_DaemonUnitStatusArray',payload);
        if(!state.daemonUnitStatusArray[payload.hostname]){return;}
        if(state.daemonUnitStatusArray[payload.hostname].length<1){return;}
        for(let i1=0;i1<state.daemonUnitStatusArray[payload.hostname].length;i1++){
            if(state.daemonUnitStatusArray[payload.hostname][i1].UnitName!==payload.UnitName){continue;}
            Vue.set(state.daemonUnitStatusArray[payload.hostname][i1],'UnitSettings',payload.UnitSettings);
            return;
        }
    }*/
};
