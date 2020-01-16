<template>
    <div class="components" data-component="daemon-controller">
        <p v-if="!currentDaemonItem" class="no-selected-notice">Not selected any daemon</p>
        <p v-else-if="!currentDaemonItem.websocketWrap || !currentDaemonItem.websocketWrap.connected" class="no-selected-notice">Not connected daemon</p>
        <p v-else-if="currentDaemonItem.websocketWrap.connected && !currentDaemonItem.websocketWrap.connectionValid" class="no-selected-notice">Daemon connection is not validated</p>
        <div v-else class="daemon-unit-list">
            <Row v-if="currentDaemonItem.unitsStatusArray.length>0" v-bind:gutter="8" type="flex" align="top" class="daemon-unit-list-row">
                <i-col v-bind:xs="24" v-bind:md="12" v-bind:lg="8" v-bind:xxl="6"
                v-for="unitStatusItem in currentDaemonItem.unitsStatusArray" v-bind:key="unitStatusItem.unitName" class-name="daemon-unit-item">
                    <Card class="daemon-unit-item-card">
                        <p slot="title" v-text="unitStatusItem.unitName" />
                        <span slot="extra">
                            <Button slot="extra" v-on:click="reloadUnitSettings(unitStatusItem)" type="text" size="small">reload</Button>
                        </span>
                        <CellGroup class="daemon-unit-item-card-cellgroup-details">
                            <Cell v-bind:label="unitStatusItem.unitSettings.description" title="Description" />
                            <Cell v-bind:label="unitStatusItem.unitSettings.executeAbsolutePath" title="ExecuteAbsolutePath" />
                            <Cell v-bind:label="unitStatusItem.unitSettings.workAbsoluteDirectory" title="WorkAbsoluteDirectory" />
                            <Cell v-bind:label="unitStatusItem.unitSettings.executeParams | unitSettingsExecuteParamsFilter" title="ExecuteParams" />
                            <Cell v-bind:extra="unitStatusItem.unitSettings.autoStart | unitSettingsAutoStartFilter" title="AutoStart" />
                            <Cell v-bind:extra="unitStatusItem.unitSettings.autoStartDelay | unitSettingsAutoStartDelayFilter" title="AutoStartDelay" />
                            <Cell v-bind:extra="unitStatusItem.unitSettings.daemonProcess | unitSettingsDaemonProcessFilter" title="DaemonProcess" />
                            <Cell v-bind:extra="unitStatusItem.unitSettings.haveChildProcesses | unitSettingsHaveChildProcessesFilter" title="HaveChildProcesses" />
                        </CellGroup>
                        <Divider />
                        <CellGroup class="daemon-unit-item-card-cellgroup-actions">
                            <Cell v-bind:extra="unitStatusItem.unitProcess.processId | unitProcessProcessIdFilter" title="ProcessId" />
                            <Cell title="IsRunning">
                                <i-switch slot="extra" v-bind:value="unitStatusItem.unitProcess.state | unitProcessStateFilter"
                                v-bind:before-change="beforeChangeUnitStatusUnitProcessState"
                                v-on:click.native="toggleChangeUnitStatusUnitProcessState(unitStatusItem)" />
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
        unitSettingsExecuteParamsFilter:function(value){return value||'No Params';},
        unitSettingsAutoStartDelayFilter:function(value){
            if(value<1){return 'No Delay';}
            if(value===1){return '1 second';}
            return value+' seconds';
        },
        unitSettingsAutoStartFilter:function(value){return value?'Enabled':'Disabled';},
        unitSettingsDaemonProcessFilter:function(value){return value?'Enabled':'Disabled';},
        unitSettingsHaveChildProcessesFilter:function(value){return value?'Yes':'No';},
        unitProcessProcessIdFilter:function(value){return value.toString();},
        unitProcessStateFilter:function(value){return value===3;}
    },
    data:function(){
        return {
            actionUnitName:null
        };
    },
    computed:{
        currentDaemonItem:function(){return this.$store.state.currentDaemonItem;}
    },
    methods:{
        setActionUnitName:function(unitName){this.actionUnitName=unitName;},
        beforeChangeUnitStatusUnitProcessState:function(){return new Promise(function(resolve){});},
        toggleChangeUnitStatusUnitProcessState:function(unitStatusItem){
            this.setActionUnitName(unitStatusItem.unitName);
            if(unitStatusItem.unitProcess.state===3){
                this.currentDaemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestStopUnit',{unitName:unitStatusItem.unitName});
            }else{
                this.currentDaemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestStartUnit',{unitName:unitStatusItem.unitName});
            }
        },
        reloadUnitSettings:function(unitStatusItem){
            this.setActionUnitName(unitStatusItem.unitName);
            this.currentDaemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestReloadUnitSettings',{unitName:unitStatusItem.unitName});
        }
    }
};
</script>
