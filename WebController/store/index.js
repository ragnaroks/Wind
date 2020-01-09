import Vue from 'vue';

export const state= () => ({
    currentWebSocketItemHostname:null,
    webSocketMessageSplitChar:'§',
    webSocketArray:[
        {
            hostname:'localhost',
            instance:null,
            connected:false,
            address:'ws://localhost:25565',
            controlKey:'https://github.com/ragnaroks/Wind2',
            connectionId:null,
            connectionValid:false,
            recvivedText:'',
            sentText:'',
            recvivedLength:0,
            sentLength:0
        },{
            hostname:'127.0.0.1',
            instance:null,
            connected:false,
            address:'ws://127.0.0.1:25565',
            controlKey:'https://github.com/ragnaroks/Wind2',
            connectionId:null,
            connectionValid:false,
            recvivedText:'',
            sentText:'',
            recvivedLength:0,
            sentLength:0
        },{
            hostname:'::1',
            instance:null,
            connected:false,
            address:'ws://[::1]:25565',
            controlKey:'https://github.com/ragnaroks/Wind2',
            connectionId:null,
            connectionValid:false,
            recvivedText:'',
            sentText:'',
            recvivedLength:0,
            sentLength:0
        }
    ],
    daemonUnitStatusArray:{}
});

export const getters = {
    get_WebSocketItem_ByConnectionId:function(state){
        return function(connectionId){
            for(let i1=0;i1<state.webSocketArray.length;i1++){
                if(state.webSocketArray[i1].connectionId!==connectionId){continue;}
                return state.webSocketArray[i1];
            }
        };
    }
};

export const mutations = {
    setCurrentWebSocketItemHostname:function(state,payload){
        if(!payload.hostname){return;}
        state.currentWebSocketItemHostname=payload.hostname;
    },
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
    }
};
