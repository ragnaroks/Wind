<template>
    <Card class="components" data-component="daemon-unit-item">
        <p slot="title" v-text="unitStatusItem.unitName" />
        <span slot="extra">
            <Button slot="extra" v-on:click="reloadUnitSettings" type="text" size="small">reload</Button>
        </span>
        <CellGroup class="daemon-unit-item-cellgroup">
            <Cell v-bind:extra="unitStatusItem.unitProcess.processId | unitProcessProcessIdFilter" title="ProcessId" />
            <Cell title="IsRunning">
                <i-switch slot="extra" v-bind:value="unitStatusItem.unitProcess.state | unitProcessStateFilter"
                v-bind:before-change="beforeChangeUnitStatusUnitProcessState"
                v-on:click.native="toggleChangeUnitStatusUnitProcessState" />
            </Cell>
            <Divider size="small" class="divider1" />
            <Cell v-bind:label="unitStatusItem.unitSettings.description" title="Description" />
            <Cell v-bind:label="unitStatusItem.unitSettings.executeAbsolutePath" title="ExecuteAbsolutePath" />
            <Cell v-bind:label="unitStatusItem.unitSettings.workAbsoluteDirectory" title="WorkAbsoluteDirectory" />
            <Cell v-bind:label="unitStatusItem.unitSettings.executeParams | unitSettingsExecuteParamsFilter" title="ExecuteParams" />
            <Cell v-bind:extra="unitStatusItem.unitSettings.autoStart | unitSettingsAutoStartFilter" title="AutoStart" />
            <Cell v-bind:extra="unitStatusItem.unitSettings.autoStartDelay | unitSettingsAutoStartDelayFilter" title="AutoStartDelay" />
            <Cell v-bind:extra="unitStatusItem.unitSettings.daemonProcess | unitSettingsDaemonProcessFilter" title="DaemonProcess" />
            <Cell v-bind:extra="unitStatusItem.unitSettings.haveChildProcesses | unitSettingsHaveChildProcessesFilter" title="HaveChildProcesses" />
            <Cell v-bind:extra="unitStatusItem.unitSettings.fetchNetworkUsage | unitSettingsFetchNetworkUsageFilter" title="FetchNetworkUsage" />
            <Divider size="small" class="divider1" />
            <Cell v-bind:extra="unitStatusItem.unitNetworkCounter.totalSent | fixedByteSizeFilter" title="TotalSent" />
            <Cell v-bind:extra="unitStatusItem.unitNetworkCounter.totalReceived | fixedByteSizeFilter" title="TotalReceived" />
            <Cell v-bind:extra="unitStatusItem.unitNetworkCounter.sendSpeed | fixedByteSpeedFilter" title="SendSpeed" />
            <Cell v-bind:extra="unitStatusItem.unitNetworkCounter.receiveSpeed | fixedByteSpeedFilter" title="ReceiveSpeed" />
        </CellGroup>
    </Card>
</template>

<style scoped>
.daemon-unit-item-cellgroup{margin:-16px;}
.divider1{margin:1px 0;}
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
        unitSettingsFetchNetworkUsageFilter:function(value){return value?'Enabled':'Disabled';},
        unitProcessProcessIdFilter:function(value){return value.toString();},
        unitProcessStateFilter:function(value){return value===3;},
        fixedByteSizeFilter:function(value){
            if(!value){return '0 B';}
            if(value>0 && value<1024){return value+' B';}
            if(value>=1024 && value<1048576){return (value/1024).toFixed(2).replace('.00','')+' KiB';}
            if(value>=1048576 && value<1073741824){return (value/1048576).toFixed(2).replace('.00','')+' MiB';}
            if(value>=1073741824 && value<109951162776){return (value/1073741824).toFixed(2).replace('.00','')+' GiB';}
            return (value/1073741824).toFixed(2).replace('.00','')+' GiB';
        },
        fixedByteSpeedFilter:function(value){
            if(!value){return '0 B/s';}
            if(value>0 && value<1024){return value+' B/s';}
            if(value>=1024 && value<1048576){return (value/1024).toFixed(2).replace('.00','')+' KiB/s';}
            if(value>=1048576 && value<1073741824){return (value/1048576).toFixed(2).replace('.00','')+' MiB/s';}
            if(value>=1073741824 && value<109951162776){return (value/1073741824).toFixed(2).replace('.00','')+' GiB/s';}
            return (value/1073741824).toFixed(2).replace('.00','')+' GiB/s';
        }
    },
    props:{
        unitStatusItem:{
            type:Object,
            required:true,
            default:function(){return null;}
        },
        daemonItem:{
            type:Object,
            required:true,
            default:function(){return null;}
        }
    },
    data:function(){
        return {
            refreshTimerInterval:1000,
            refreshTimerId:null
        };
    },
    computed:{
        currentDaemonItem:function(){return this.$store.state.currentDaemonItem;}
    },
    created:function(){
        if(!this.unitStatusItem){throw new Error('unitStatusItem must be valid vlaue');}
        if(!this.daemonItem){throw new Error('daemonItem must be valid vlaue');}
        if(!this.unitStatusItem.unitSettings.fetchNetworkUsage){return;}
        this.refreshTimerStart();
    },
    beforeDestroy:function(){
        if(!this.unitStatusItem.unitSettings.fetchNetworkUsage){return;}
        this.refreshTimerStop();
    },
    methods:{
        beforeChangeUnitStatusUnitProcessState:function(){return new Promise(function(resolve){});},
        toggleChangeUnitStatusUnitProcessState:function(){
            if(this.unitStatusItem.unitProcess.state===3){
                this.daemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestStopUnit',{unitName:this.unitStatusItem.unitName});
            }else{
                this.daemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestStartUnit',{unitName:this.unitStatusItem.unitName});
            }
        },
        reloadUnitSettings:function(){
            this.daemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestReloadUnitSettings',{unitName:this.unitStatusItem.unitName});
        },
        refreshTimerStart:function(){
            const _this=this;
            this.refreshTimerId=window.setInterval(function(){
                //console.log(_this.daemonItem);
                //这里用 daemonItem.websocketWrap 来自动限定关联的daemon
                if(!_this.daemonItem || !_this.daemonItem.websocketWrap){return;}
                _this.daemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestFetchUnitStatusNetworkCounter',{unitName:_this.unitStatusItem.unitName});
            },this.refreshTimerInterval);
        },
        refreshTimerStop:function(){
            console.log('refreshTimerstop');
            window.clearInterval(this.refreshTimerId);
        }
    }
};
</script>
