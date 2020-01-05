<template>
    <div class="components" data-component="daemon-controller">
        <p v-if="!webSocketItem" class="no-selected-notice">No selected any daemon</p>
        <p v-if="!!webSocketItem && !webSocketItem.connectionValid" class="no-selected-notice">Daemon connection is not valid</p>
        <div v-if="!!webSocketItem && webSocketItem.connectionValid" class="daemon-unit-list">
            <Row v-bind:gutter="8" type="flex" align="top" class="daemon-unit-list-row">
                <i-col v-bind:xs="24" v-bind:md="12" v-bind:lg="8" v-bind:xxl="6" v-for="daemonUnit in daemonUnits" v-bind:key="daemonUnit.UnitName" class-name="daemon-unit-item">
                    <Card class="daemon-unit-item-card">
                        <p slot="title" v-text="daemonUnit.UnitName" />
                        <span slot="extra">
                            <Button slot="extra" v-on:click="daemonRefreshUnit(daemonUnit)" type="text" size="small">refresh</Button>
                        </span>
                        <CellGroup class="daemon-unit-item-card-cellgroup-details">
                            <Cell v-bind:label="daemonUnit.UnitSettings.Description" title="Description" />
                            <Cell v-bind:label="daemonUnit.UnitSettings.ExecuteAbsolutePath" title="ExecuteAbsolutePath" />
                            <Cell v-bind:label="daemonUnit.UnitSettings.WorkAbsoluteDirectory" title="WorkAbsoluteDirectory" />
                            <Cell v-bind:label="getText_UnitSettings_ExecuteParams(daemonUnit.UnitSettings)" title="ExecuteParams" />
                            <Cell title="AutoStart">
                                <i-switch slot="extra" v-model="daemonUnit.UnitSettings.AutoStart" disabled />
                            </Cell>
                            <Cell v-bind:extra="getText_UnitSettings_AutoStartDelay(daemonUnit.UnitSettings)" title="AutoStartDelay" />
                        </CellGroup>
                        <Divider />
                        <CellGroup class="daemon-unit-item-card-cellgroup-actions">
                            <!--<Cell title="Refresh Unit Settings">
                                <Button slot="extra" v-on:click="daemonRefreshUnit(daemonUnit)" type="text" size="small">refresh</Button>
                            </Cell>-->
                            <Cell title="IsRunning">
                                <i-switch slot="extra" v-model="daemonUnit.UnitStatus.IsRunning" v-bind:before-change="beforeChangeUnitStatusIsRunning(daemonUnit)" />
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
    props:{
        webSocketItem:{
            type:Object,
            required:false,
            default:function(){
                return null;
            },
            validator:function(value){
                if(!value){return true;}
                return !!value.address && value.connected!==undefined && value.connectionId!==undefined && value.connectionValid!==undefined && !!value.controlKey && !!value.hostname && value.instance!==undefined && value.recvivedLength!==undefined && value.recvivedText!==undefined && value.sentLength!==undefined && value.sentText!==undefined;
            }
        }
    },
    data:function(){
        return {
            splitChar:'§',
            daemonUnits:[
                {
                    UnitName:'webd',
                    UnitSettings:{
                        Name:'webd',
                        Description:'webd',
                        ExecuteAbsolutePath:'D:\\webd\\webd.exe',
                        WorkAbsoluteDirectory:'D:\\webd\\',
                        ExecuteParams:null,
                        AutoStart:true,
                        AutoStartDelay:5
                    },
                    UnitStatus:{
                        IsRunning:false
                    }
                    //UnitProcess:{}
                }
            ]
        };
    },
    methods:{
        getText_UnitSettings_ExecuteParams:function(unitSettings){
            if(!unitSettings.ExecuteParams){return 'No Params';}
            return unitSettings.ExecuteParams;
        },
        getText_UnitSettings_AutoStartDelay:function(unitSettings){
            if(unitSettings.AutoStartDelay<1){return 'No Delay';}
            if(unitSettings.AutoStartDelay===1){return '1 second';}
            return unitSettings.AutoStartDelay+' seconds';
        },
        daemonFetchAllUnitsSettings:function(){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'FetchAllUnitsSettings';
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        daemonFetchUnitSettings:function(daemonUnit){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'FetchUnitSettings'+this.splitChar+daemonUnit.UnitName;
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        dameonRefreshAllUnits:function(){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'RefreshAllUnits';
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        daemonStartAllUnits:function(){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'StartAllUnits';
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        daemonStopAllUnits:function(){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'StopAllUnits';
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        daemonRefreshUnit:function(daemonUnit){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'RefreshUnit'+this.splitChar+daemonUnit.UnitName;
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        daemonStartUnit:function(daemonUnit){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'StartUnit'+this.splitChar+daemonUnit.UnitName;
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        daemonStopUnit:function(daemonUnit){
            const commandText=this.webSocketItem.connectionId+this.splitChar+'StopUnit'+this.splitChar+daemonUnit.UnitName;
            this.webSocketItem.instance.send(commandText);
            this.webSocketItem.sentText+=commandText+'\n';
            this.webSocketItem.sentLength+=commandText.length;
        },
        beforeChangeUnitStatusIsRunning:function(daemonUnit){
            //
        }
    }
};
</script>
