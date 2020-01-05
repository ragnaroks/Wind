<template>
    <div class="components" data-component="daemon-controller">
        <p v-if="!currentWebSocketItem" class="no-selected-notice">No selected any daemon</p>
        <p v-if="currentWebSocketItem && !currentWebSocketItem.connectionValid" class="no-selected-notice">Daemon connection is not valid</p>
        <div v-if="currentWebSocketItem && currentWebSocketItem.connectionValid" class="daemon-unit-list">
            <Row v-if="currentDaemonUnitsStatusArray && currentDaemonUnitsStatusArray.length>0" v-bind:gutter="8" type="flex" align="top" class="daemon-unit-list-row">
                <i-col v-bind:xs="24" v-bind:md="12" v-bind:lg="8" v-bind:xxl="6"
                v-for="currentDaemonUnitStatusItem in currentDaemonUnitsStatusArray" v-bind:key="currentDaemonUnitStatusItem.UnitName"
                class-name="daemon-unit-item">
                    <Card class="daemon-unit-item-card">
                        <p slot="title" v-text="currentDaemonUnitStatusItem.UnitName" />
                        <span slot="extra">
                            <Button slot="extra" v-on:click="daemonReloadUnit(currentDaemonUnitStatusItem)" type="text" size="small">reload</Button>
                        </span>
                        <CellGroup class="daemon-unit-item-card-cellgroup-details">
                            <Cell v-bind:label="currentDaemonUnitStatusItem.UnitSettings.Description" title="Description" />
                            <Cell v-bind:label="currentDaemonUnitStatusItem.UnitSettings.ExecuteAbsolutePath" title="ExecuteAbsolutePath" />
                            <Cell v-bind:label="currentDaemonUnitStatusItem.UnitSettings.WorkAbsoluteDirectory" title="WorkAbsoluteDirectory" />
                            <Cell v-bind:label="currentDaemonUnitStatusItem.UnitSettings.ExecuteParams | textFilterUnitSettingsExecuteParams" title="ExecuteParams" />
                            <Cell title="AutoStart">
                                <i-switch slot="extra" v-bind:value="currentDaemonUnitStatusItem.UnitSettings.AutoStart" disabled />
                            </Cell>
                            <Cell v-bind:extra="currentDaemonUnitStatusItem.UnitSettings.AutoStartDelay | textFilterUnitSettingsAutoStartDelay" title="AutoStartDelay" />
                        </CellGroup>
                        <Divider />
                        <CellGroup class="daemon-unit-item-card-cellgroup-actions">
                            <Cell v-bind:extra="getFixedUnitPorcessProcessId(currentDaemonUnitStatusItem)" title="ProcessId" />
                            <Cell title="IsRunning">
                                <i-switch slot="extra" v-bind:true-value="3" v-bind:false-value="1"
                                v-bind:value="getFixedUnitPorcessState(currentDaemonUnitStatusItem)"
                                v-bind:before-change="beforeChangeUnitStatusUnitProcessState"
                                v-on:click.native="toggleChangeUnitStatusUnitProcessState(currentDaemonUnitStatusItem)" />
                            </Cell>
                        </CellGroup>
                    </Card>
                </i-col>
            </Row>
        </div>
    </div>
</template>

<style scoped>
.components .no-selected-notice{padding:1rem;}
.daemon-unit-list-row{padding:0.5rem;}
.daemon-unit-item{margin-bottom:0.5rem;}
.daemon-unit-item-card-cellgroup-details{margin:-16px;}
.daemon-unit-item-card-cellgroup-actions{margin:-16px;}
</style>

<script>
export default {
    filters:{
        textFilterUnitSettingsExecuteParams:function(value){return value||'No Params';},
        textFilterUnitSettingsAutoStartDelay:function(value){
            if(value<1){return 'No Delay';}
            if(value===1){return '1 second';}
            return value+' seconds';
        }
    },
    data:function(){
        return {
            actionUnitName:null
        };
    },
    computed:{
        webSocketMessageSplitChar:function(){return this.$store.state.webSocketMessageSplitChar;},
        currentWebSocketItem:function(){
            if(this.$store.state.webSocketArray.length<1){return null;}
            for(let i1=0;i1<this.$store.state.webSocketArray.length;i1++){
                if(this.$store.state.webSocketArray[i1].hostname!==this.$store.state.currentWebSocketItemHostname){continue;}
                return this.$store.state.webSocketArray[i1];
            }
            return null;
        },
        currentDaemonUnitsStatusArray:function(){return this.$store.state.daemonUnitStatusArray[this.currentWebSocketItem.hostname];}
    },
    methods:{
        setActionUnitName:function(unitName){this.actionUnitName=unitName;},
        beforeChangeUnitStatusUnitProcessState:function(){
            return new Promise(function(resolve){});
        },
        toggleChangeUnitStatusUnitProcessState:function(currentDaemonUnitStatusItem){
            this.setActionUnitName(currentDaemonUnitStatusItem.UnitName);
            if(this.getFixedUnitPorcessState(currentDaemonUnitStatusItem)===3){
                this.daemonStopUnit(currentDaemonUnitStatusItem);
            }else{
                this.daemonStartUnit(currentDaemonUnitStatusItem);
            }
        },
        getFixedUnitPorcessState:function(currentDaemonUnitStatusItem){
            if(!currentDaemonUnitStatusItem.UnitProcess || !currentDaemonUnitStatusItem.UnitProcess.State){return 1;}
            return currentDaemonUnitStatusItem.UnitProcess.State===3?3:1;
        },
        getFixedUnitPorcessProcessId:function(currentDaemonUnitStatusItem){
            if(!currentDaemonUnitStatusItem.UnitProcess || !currentDaemonUnitStatusItem.UnitProcess.ProcessId){return '0';}
            return currentDaemonUnitStatusItem.UnitProcess.ProcessId.toString();
        },
        daemonFetchAllUnits:function(){
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'FetchAllUnitsStatus';
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        },
        daemonFetchUnit:function(currentDaemonUnitStatusItem){
            console.log('daemonFetchUnit',currentDaemonUnitStatusItem);
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'FetchUnitStatus'+this.webSocketMessageSplitChar+currentDaemonUnitStatusItem.UnitName;
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        },
        dameonReloadAllUnits:function(){
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'ReloadAllUnits';
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        },
        daemonStartAllUnits:function(){
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'StartAllUnits';
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        },
        daemonStopAllUnits:function(){
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'StopAllUnits';
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        },
        daemonReloadUnit:function(currentDaemonUnitStatusItem){
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'ReloadUnit'+this.webSocketMessageSplitChar+currentDaemonUnitStatusItem.UnitName;
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        },
        daemonStartUnit:function(currentDaemonUnitStatusItem){
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'StartUnit'+this.webSocketMessageSplitChar+currentDaemonUnitStatusItem.UnitName;
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        },
        daemonStopUnit:function(currentDaemonUnitStatusItem){
            const commandText=this.currentWebSocketItem.connectionId+this.webSocketMessageSplitChar+'StopUnit'+this.webSocketMessageSplitChar+currentDaemonUnitStatusItem.UnitName;
            this.currentWebSocketItem.instance.send(commandText);
            this.$store.commit('append_WebSocketItem_SentText',{hostname:this.currentWebSocketItem.hostname,sentText:commandText+'\n'});
            this.$store.commit('increase_WebSocketItem_SentLength',{hostname:this.currentWebSocketItem.hostname,sentLength:commandText.length});
        }
    }
};
</script>
