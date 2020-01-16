export const state= () => ({
    currentDaemonHostname:null,
    currentDaemonItem:null,//应该只读
    daemonArray:[
        {
            hostname:'localhost',
            //
            websocketAddress:'ws://localhost:25565',
            websocketControlKey:'https://github.com/ragnaroks/Wind2',
            websocketWrap:null,
            //
            daemonVersion:null,
            daemonWorkDirectory:null,
            daemonHostCpuCores:null,
            daemonHostMemorySize:null,
            daemonProcessId:null,
            //
            daemonProcessTimePercentage:null,
            daemonProcessWorkingSetSize:null,
            daemonUnitSettingsCount:null,
            daemonUnitProcessCount:null,
            //
            unitsStatusArray:[]
        },{
            hostname:'127.0.0.1',
            //
            websocketAddress:'ws://127.0.0.1:25565',
            websocketControlKey:'https://github.com/ragnaroks/Wind2',
            websocketWrap:null,
            //
            daemonVersion:null,
            daemonWorkDirectory:null,
            daemonHostCpuCores:null,
            daemonHostMemorySize:null,
            daemonProcessId:null,
            //
            daemonProcessTimePercentage:null,
            daemonProcessWorkingSetSize:null,
            daemonUnitSettingsCount:null,
            daemonUnitProcessCount:null,
            //
            unitsStatusArray:[]
        },{
            hostname:'::1',
            //
            websocketAddress:'ws://[::1]:25565',
            websocketControlKey:'https://github.com/ragnaroks/Wind2',
            websocketWrap:null,
            //
            daemonVersion:null,
            daemonWorkDirectory:null,
            daemonHostCpuCores:null,
            daemonHostMemorySize:null,
            daemonProcessId:null,
            //
            daemonProcessTimePercentage:null,
            daemonProcessWorkingSetSize:null,
            daemonUnitSettingsCount:null,
            daemonUnitProcessCount:null,
            //
            unitsStatusArray:[]
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
    set_daemonMetaValues_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].daemonVersion=payload.daemonVersion;
            state.daemonArray[i1].daemonWorkDirectory=payload.daemonWorkDirectory;
            state.daemonArray[i1].daemonHostCpuCores=payload.daemonHostCpuCores;
            state.daemonArray[i1].daemonHostMemorySize=payload.daemonHostMemorySize;
            state.daemonArray[i1].daemonProcessId=payload.daemonProcessId;
            break;
        }
    },
    set_daemonStatusValues_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].daemonProcessTimePercentage=payload.daemonProcessTimePercentage;
            state.daemonArray[i1].daemonProcessWorkingSetSize=payload.daemonProcessWorkingSetSize;
            state.daemonArray[i1].daemonUnitSettingsCount=payload.daemonUnitSettingsCount;
            state.daemonArray[i1].daemonUnitProcessCount=payload.daemonUnitProcessCount;
            break;
        }
    },
    set_unitsStatusArray_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].unitsStatusArray=payload.unitsStatusArray;
            break;
        }
    },
    clear_unitsStatusArray_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            state.daemonArray[i1].unitsStatusArray=[];
            break;
        }
    },
    set_unitStatusItem_In_unitsStatusArray_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        let unitsStatusArray=null;
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            unitsStatusArray=state.daemonArray[i1].unitsStatusArray;
            break;
        }
        if(unitsStatusArray===null || unitsStatusArray.length<1){return;}
        for(let i2=0;i2<unitsStatusArray.length;i2++){
            if(unitsStatusArray[i2].unitName!==payload.unitStatusItem.unitName){continue;}
            unitsStatusArray[i2]=payload.unitStatusItem;
            break;
        }
    },
    set_unitSettings_In_unitStatusItem_In_unitsStatusArray_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        let unitsStatusArray=null;
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            unitsStatusArray=state.daemonArray[i1].unitsStatusArray;
            break;
        }
        if(unitsStatusArray===null || unitsStatusArray.length<1){return;}
        for(let i2=0;i2<unitsStatusArray.length;i2++){
            if(unitsStatusArray[i2].unitName!==payload.unitSettings.name){continue;}
            unitsStatusArray[i2].unitSettings=payload.unitSettings;
            break;
        }
    },
    set_unitProcess_In_unitStatusItem_In_unitsStatusArray_In_DaemonItem:function(state,payload){
        if(!payload.hostname){return;}
        if(state.daemonArray.length<1){return;}
        let unitsStatusArray=null;
        for(let i1=0;i1<state.daemonArray.length;i1++){
            if(state.daemonArray[i1].hostname!==payload.hostname){continue;}
            unitsStatusArray=state.daemonArray[i1].unitsStatusArray;
            break;
        }
        if(unitsStatusArray===null || unitsStatusArray.length<1){return;}
        for(let i2=0;i2<unitsStatusArray.length;i2++){
            if(unitsStatusArray[i2].unitName!==payload.unitProcess.name){continue;}
            unitsStatusArray[i2].unitProcess=payload.unitProcess;
            break;
        }
    }
};
