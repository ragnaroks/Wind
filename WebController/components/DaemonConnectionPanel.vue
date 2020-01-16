<template>
    <div class="components" data-component="daemon-connection-panel">
        <p v-if="!currentDaemonItem" class="no-selected-notice">No selected any daemon</p>
        <CellGroup v-if="currentDaemonItem">
            <Cell v-bind:extra="currentDaemonItem.hostname" title="Hostname">
                <Icon slot="icon" type="logo-windows" />
            </Cell>
            <Cell v-bind:extra="currentDaemonItem.websocketAddress" title="Address">
                <Icon slot="icon" type="md-at" />
            </Cell>
            <Cell v-bind:extra="currentDaemonItem.websocketControlKey" title="ControlKey">
                <Icon slot="icon" type="md-key" />
            </Cell>
            <Cell title="IsConnect">
                <Icon slot="icon" type="ios-wifi" />
                <i-switch slot="extra" v-bind:value="currentDaemonItem.websocketWrap.connected"
                v-bind:before-change="beforeChangeCurrentDaemonConnected"
                v-on:click.native="toggleChangeCurrentDaemonConnected" />
            </Cell>
            <Cell title="ConnectionId">
                <Icon slot="icon" type="md-contact" />
                <span slot="extra" v-text="currentDaemonItem.websocketWrap.connected?currentDaemonItem.websocketWrap.connectionId:'No Connection'" />
            </Cell>
            <Cell title="IsConnectionValidated">
                <Icon slot="icon" type="md-checkmark-circle" />
                <span slot="extra" v-show="!currentDaemonItem.websocketWrap.connected">No Connection</span>
                <span slot="extra" v-show="currentDaemonItem.websocketWrap.connected && !currentDaemonItem.websocketWrap.connectionValid" class="current-web-socket-item-connection-invalid">Invalid</span>
                <span slot="extra" v-show="currentDaemonItem.websocketWrap.connected && currentDaemonItem.websocketWrap.connectionValid" class="current-web-socket-item-connection-valid">Valid</span>
            </Cell>
            <Cell v-bind:extra="currentDaemonItem.websocketWrap.sentLength | fixedByteSizeFilter" title="Sent Byte">
                <Icon slot="icon" type="md-arrow-round-up" />
            </Cell>
            <Cell v-bind:extra="currentDaemonItem.websocketWrap.receivedLength | fixedByteSizeFilter" title="Received Byte">
                <Icon slot="icon" type="md-arrow-round-down" />
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
    filters:{
        fixedByteSizeFilter:function(value){
            if(!value){return '0 B';}
            if(value>0 && value<1024){return value+' B';}
            if(value>=1024 && value<1048576){return (value/1024).toFixed(2).replace('.00','')+' KiB';}
            if(value>=1048576 && value<1073741824){return (value/1048576).toFixed(2).replace('.00','')+' MiB';}
            if(value>=1073741824 && value<109951162776){return (value/1073741824).toFixed(2).replace('.00','')+' GiB';}
            return (value/1073741824).toFixed(2).replace('.00','')+' GiB';
        }
    },
    computed:{
        currentDaemonHostname:function(){return this.$store.state.currentDaemonHostname;},
        currentDaemonItem:function(){return this.$store.state.currentDaemonItem;}
    },
    methods:{
        beforeChangeCurrentDaemonConnected:function(){return new Promise(function(resolve){});},
        toggleChangeCurrentDaemonConnected:function(){
            if(this.currentDaemonItem.websocketWrap.connected){
                this.currentDaemonItem.websocketWrap.Close();
                return;
            }
            try{
                this.currentDaemonItem.websocketWrap.Connect();
            }catch(error){
                console.error('websocket connect error',error);
                this.$Notice.error({title:this.currentDaemonHostname,desc:'connection on error,see details in devtool(F12)',duration:0});
            }
        }
        /*
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
            if(!argsArray[2] || !argsArray[3]){return;}
            const unitSettings=JSON.parse(argsArray[3]);
            if(!unitSettings){return;}
            this.$store.commit('set_Unit_UnitSettings_In_DaemonUnitStatusArray',{hostname:this.currentWebSocketItem.hostname,UnitName:argsArray[2],UnitSettings:unitSettings});
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
        }*/
    }
};
</script>
