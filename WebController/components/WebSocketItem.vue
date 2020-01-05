<template>
    <div class="components" data-component="web-socket-item">
        <p v-if="!currentWebSocketItem" class="no-selected-notice">No selected any daemon</p>
        <CellGroup v-if="currentWebSocketItem">
            <Cell v-bind:extra="currentWebSocketItem.hostname" title="Hostname" />
            <Cell title="IsConnect">
                <i-switch slot="extra" v-bind:value="currentWebSocketItem.connected"
                v-bind:before-change="beforeChangeCurrentWebSocketItemConnected"
                v-on:click.native="toggleChangeCurrentWebSocketItemConnected" />
            </Cell>
            <Cell v-bind:extra="currentWebSocketItem.address" title="Address" />
            <Cell v-bind:extra="currentWebSocketItem.controlKey" title="ControlKey" />
            <Cell title="ConnectionId">
                <span slot="extra" v-show="!currentWebSocketItem.connected">No Connected</span>
                <span slot="extra" v-show="currentWebSocketItem.connected" v-text="currentWebSocketItem.connectionId" />
            </Cell>
            <Cell title="IsConnectionValidated">
                <span slot="extra" v-show="!currentWebSocketItem.connected">No Connected</span>
                <span slot="extra" v-show="currentWebSocketItem.connected && !currentWebSocketItem.connectionValid" class="current-web-socket-item-connection-invalid">Invalid</span>
                <span slot="extra" v-show="currentWebSocketItem.connected && currentWebSocketItem.connectionValid" class="current-web-socket-item-connection-valid">Valid</span>
            </Cell>
        </CellGroup>
    </div>
</template>

<style scoped>
.components .no-selected-notice{padding:1rem;}
.current-web-socket-item-connection-invalid{color:red;}
.current-web-socket-item-connection-valid{color:green;}
</style>

<script>
export default {
    computed:{
        webSocketMessageSplitChar:function(){return this.$store.state.webSocketMessageSplitChar;},
        currentWebSocketItem:function(){
            if(this.$store.state.webSocketArray.length<1){return null;}
            for(let i1=0;i1<this.$store.state.webSocketArray.length;i1++){
                if(this.$store.state.webSocketArray[i1].hostname!==this.$store.state.currentWebSocketItemHostname){continue;}
                return this.$store.state.webSocketArray[i1];
            }
            return null;
        }/*,
        currentWebSocketItemConnected:{
            get:function(){return this.currentWebSocketItem.connected;},
            set:function(value){
                if(this.currentWebSocketItem.connected){
                    this.currentWebSocketItem.instance.close(1000,'user-close');
                    this.$store.commit('set_WebSocketItem_Connected',{hostname:this.currentWebSocketItem.hostname,connected:false});
                }else{
                    try{
                        this.$store.commit('set_WebSocketItem_Instance',{hostname:this.currentWebSocketItem.hostname,instance:new WebSocket(this.currentWebSocketItem.address)});
                    }catch(exception){
                        console.error(exception);
                        this.$Notice.error({
                            title:this.currentWebSocketItem.hostname,
                            desc:this.currentWebSocketItem.hostname+' can not connect (see details in devtool)'
                        });
                    }
                    const _this=this;
                    _this.currentWebSocketItem.instance.onopen=function(onopenEvent){_this.webSocketItemOnOpen(onopenEvent);};
                    _this.currentWebSocketItem.instance.onerror=function(onerrorEvent){_this.webSocketItemOnError(onerrorEvent);};
                    _this.currentWebSocketItem.instance.onclose=function(oncloseEvent){_this.webSocketItemOnClose(oncloseEvent);};
                    _this.currentWebSocketItem.instance.onmessage=function(onmessageEvent){_this.webSocketItemOnMessage(onmessageEvent);};
                }
            }
        }*/
    },
    methods:{
        beforeChangeCurrentWebSocketItemConnected:function(){
            return new Promise(function(resolve){});
        },
        toggleChangeCurrentWebSocketItemConnected:function(){
            if(this.currentWebSocketItem.connected){
                this.currentWebSocketItem.instance.close(1000,'user-close');
                this.$store.commit('set_WebSocketItem_Connected',{hostname:this.currentWebSocketItem.hostname,connected:false});
            }else{
                try{
                    this.$store.commit('set_WebSocketItem_Instance',{hostname:this.currentWebSocketItem.hostname,instance:new WebSocket(this.currentWebSocketItem.address)});
                }catch(exception){
                    console.error(exception);
                    this.$Notice.error({
                        title:this.currentWebSocketItem.hostname,
                        desc:this.currentWebSocketItem.hostname+' can not connect (see details in devtool)'
                    });
                }
                const _this=this;
                _this.currentWebSocketItem.instance.onopen=function(onopenEvent){_this.webSocketItemOnOpen(onopenEvent);};
                _this.currentWebSocketItem.instance.onerror=function(onerrorEvent){_this.webSocketItemOnError(onerrorEvent);};
                _this.currentWebSocketItem.instance.onclose=function(oncloseEvent){_this.webSocketItemOnClose(oncloseEvent);};
                _this.currentWebSocketItem.instance.onmessage=function(onmessageEvent){_this.webSocketItemOnMessage(onmessageEvent);};
            }
        },
        webSocketItemOnOpen:function(onopenEvent){
            console.log(onopenEvent);
            this.$store.commit('set_WebSocketItem_Connected',{hostname:this.currentWebSocketItem.hostname,connected:true});
            this.$Notice.info({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' has been connected'
            });
        },
        webSocketItemOnError:function(onerrorEvent){
            console.error(onerrorEvent);
            this.currentWebSocketItem.instance.close(1000,'error-close');
            this.$Notice.error({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' has been disconnected because an error (see details in devtool)'
            });
        },
        webSocketItemOnClose:function(oncloseEvent){
            console.log(oncloseEvent);
            this.$store.commit('set_WebSocketItem_Connected',{hostname:this.currentWebSocketItem.hostname,connected:false});
            this.$store.commit('set_WebSocketItem_ConnectionId',{hostname:this.currentWebSocketItem.hostname,connectionId:null});
            this.$store.commit('set_WebSocketItem_ConnectionValid',{hostname:this.currentWebSocketItem.hostname,connectionValid:false});
            this.$store.commit('clear_Units_In_DaemonUnitStatusArray',{hostname:this.currentWebSocketItem.hostname});
            if(oncloseEvent.code!==1000){return;}
            this.$Notice.info({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' has been disconnected'
            });
        },
        webSocketItemOnMessage:function(onmessageEvent){
            console.log(onmessageEvent);
            if(onmessageEvent.data===null || onmessageEvent.data.length<1){return;}
            this.$store.commit('append_WebSocketItem_RecvivedText',{hostname:this.currentWebSocketItem.hostname,recvivedText:onmessageEvent.data+'\n'});
            this.$store.commit('increase_WebSocketItem_RecvivedLength',{hostname:this.currentWebSocketItem.hostname,recvivedLength:onmessageEvent.data.length});
            const dataArray=onmessageEvent.data.split(this.webSocketMessageSplitChar);
            const argsArray=[];
            for(let i1=0;i1<dataArray.length;i1++){
                if(!dataArray[i1]){continue;}
                argsArray.push(dataArray[i1]);
            }
            if(argsArray.length<2){return;}
            switch(argsArray[1]){
                case 'NotifySocketOpened':this.webSocketItemOnMessage_NotifySocketOpened(argsArray);break;
                case 'NotifyCheckControlKey':this.webSocketItemOnMessage_NotifyCheckControlKey(argsArray);break;
                case 'NotifyReloadUnit':this.webSocketItemOnMessage_NotifyReloadUnit(argsArray);break;
                case 'NotifyStartUnit':this.webSocketItemOnMessage_NotifyStartUnit(argsArray);break;
                case 'NotifyStopUnit':this.webSocketItemOnMessage_NotifyStopUnit(argsArray);break;
                case 'NotifyStartUnitFailed':this.webSocketItemOnMessage_NotifyStartUnitFailed(argsArray);break;
                case 'NotifyStopUnitFailed':this.webSocketItemOnMessage_NotifyStopUnitFailed(argsArray);break;
                case 'NotifyFetchAllUnitsStatus':this.webSocketItemOnMessage_NotifyFetchAllUnitsStatus(argsArray);break;
                case 'NotifyFetchUnitStatus':this.webSocketItemOnMessage_NotifyFetchUnitStatus(argsArray);break;
                default:break;
            }
        },
        webSocketItemOnMessage_NotifySocketOpened:function(argsArray){
            this.$store.commit('set_WebSocketItem_ConnectionId',{hostname:this.currentWebSocketItem.hostname,connectionId:argsArray[0]});
            this.currentWebSocketItem.instance.send(this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'CheckControlKey'+this.webSocketMessageSplitChar+this.currentWebSocketItem.controlKey);
        },
        webSocketItemOnMessage_NotifyCheckControlKey:function(argsArray){
            if(argsArray[2]!=='success'){
                this.currentWebSocketItem.instance.close(1000,'invalid control key');
                this.$Notice.warning({
                    title:this.currentWebSocketItem.hostname,
                    desc:this.currentWebSocketItem.hostname+' has been disconnected because invalid control key'
                });
                return;
            }
            this.$store.commit('set_WebSocketItem_ConnectionValid',{hostname:this.currentWebSocketItem.hostname,connectionValid:true});
            this.$emit('webSocketItemConnectionValidated',this.currentWebSocketItem.hostname);
            this.$Notice.success({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' has been validated this connection'
            });
        },
        webSocketItemOnMessage_NotifyReloadUnit:function(argsArray){
            if(!argsArray[2]){return;}
            this.$Notice.info({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' unit ['+argsArray[2]+'] has been reload'
            });
        },
        webSocketItemOnMessage_NotifyStartUnit:function(argsArray){
            if(!argsArray[2]){return;}
            const notifyWebSocketItem=this.$store.getters.get_WebSocketItem_ByConnectionId(argsArray[0]);
            if(!notifyWebSocketItem){return;}
            const unitProcess=JSON.parse(argsArray[2]);
            if(!unitProcess || !unitProcess.Name || !unitProcess.State || unitProcess.ProcessId===undefined){return;}
            this.$store.commit('set_Unit_UnitProcess_State_In_DaemonUnitStatusArray',{
                hostname:notifyWebSocketItem.hostname,//this.currentWebSocketItem.hostname,
                UnitName:unitProcess.Name,
                State:3,//unitProcess.State
                ProcessId:unitProcess.ProcessId});
            this.$Notice.info({
                title:notifyWebSocketItem.hostname,//this.currentWebSocketItem.hostname,
                desc:notifyWebSocketItem.hostname+' unit ['+unitProcess.Name+'] has been started'//this.currentWebSocketItem.hostname+' unit ['+unitProcess.Name+'] has been started'
            });
        },
        webSocketItemOnMessage_NotifyStopUnit:function(argsArray){
            if(!argsArray[2]){return;}
            const notifyWebSocketItem=this.$store.getters.get_WebSocketItem_ByConnectionId(argsArray[0]);
            if(!notifyWebSocketItem){return;}
            this.$store.commit('set_Unit_UnitProcess_State_In_DaemonUnitStatusArray',{
                hostname:notifyWebSocketItem.hostname,//this.currentWebSocketItem.hostname,
                UnitName:argsArray[2],
                State:1,
                ProcessId:0});
            this.$Notice.info({
                title:notifyWebSocketItem.hostname,//this.currentWebSocketItem.hostname,
                desc:notifyWebSocketItem.hostname+' unit ['+argsArray[2]+'] has been stopped'//this.currentWebSocketItem.hostname+' unit ['+argsArray[2]+'] has been stopped'
            });
        },
        webSocketItemOnMessage_NotifyStartUnitFailed:function(argsArray){
            if(!argsArray[2]){return;}
            this.$store.commit('set_Unit_UnitProcess_State_In_DaemonUnitStatusArray',{
                hostname:this.currentWebSocketItem.hostname,
                UnitName:argsArray[2],
                State:1,
                ProcessId:0});
            this.$Notice.warning({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' unit ['+argsArray[2]+'] start failed'
            });
        },
        webSocketItemOnMessage_NotifyStopUnitFailed:function(argsArray){
            if(!argsArray[2]){return;}
            this.$store.commit('set_Unit_UnitProcess_State_In_DaemonUnitStatusArray',{
                hostname:this.currentWebSocketItem.hostname,
                UnitName:argsArray[2],
                State:3,
                ProcessId:undefined});
            this.$Notice.warning({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' unit ['+argsArray[2]+'] stop failed'
            });
        },
        webSocketItemOnMessage_NotifyFetchAllUnitsStatus:function(argsArray){
            if(!argsArray[2]){return;}
            const unitStatusArray=JSON.parse(argsArray[2]);
            if(!unitStatusArray || unitStatusArray.length<1){
                this.$Notice.info({
                    title:this.currentWebSocketItem.hostname,
                    desc:this.currentWebSocketItem.hostname+' can not fetch any unit status'
                });
                return;
            }
            for(let i1=0;i1<unitStatusArray.length;i1++){
                if(!unitStatusArray[i1].UnitName || !unitStatusArray[i1].UnitSettings){continue;}
                if(!unitStatusArray[i1].UnitSettings.Name || !unitStatusArray[i1].UnitSettings.ExecuteAbsolutePath || !unitStatusArray[i1].UnitSettings.WorkAbsoluteDirectory){continue;}
                if(unitStatusArray[i1].UnitSettings.Description===undefined || unitStatusArray[i1].UnitSettings.ExecuteParams===undefined || unitStatusArray[i1].UnitSettings.AutoStart===undefined || unitStatusArray[i1].UnitSettings.AutoStartDelay===undefined){continue;}
                this.$store.commit('set_Unit_To_DaemonUnitStatusArray',{
                    hostname:this.currentWebSocketItem.hostname,
                    UnitName:unitStatusArray[i1].UnitName,
                    unitStatusItem:unitStatusArray[i1]});
            }
            this.$Notice.success({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' fetched all units status'
            });
        },
        webSocketItemOnMessage_NotifyFetchUnitStatus:function(argsArray){
            if(!argsArray[2]){return;}
            const unitStatusItem=JSON.parse(argsArray[2]);
            if(!unitStatusItem || !unitStatusItem.UnitName || !unitStatusItem.UnitSettings || !unitStatusItem.UnitSettings.Name ||
            !unitStatusItem.UnitSettings.ExecuteAbsolutePath || !unitStatusItem.UnitSettings.WorkAbsoluteDirectory ||
            unitStatusItem.UnitSettings.Description===undefined || unitStatusItem.UnitSettings.ExecuteParams===undefined ||
            unitStatusItem.UnitSettings.AutoStart===undefined || unitStatusItem.UnitSettings.AutoStartDelay===undefined){
                this.$Notice.info({
                    title:this.currentWebSocketItem.hostname,
                    desc:this.currentWebSocketItem.hostname+' can not fetch unit status'
                });
                return;
            }
            this.$store.commit('set_Unit_To_DaemonUnitStatusArray',{
                    hostname:this.currentWebSocketItem.hostname,
                    UnitName:unitStatusItem.UnitName,
                    unitStatusItem:unitStatusItem});
            this.$Notice.success({
                title:this.currentWebSocketItem.hostname,
                desc:this.currentWebSocketItem.hostname+' fetched unit status'
            });
        }
    }
};
</script>
