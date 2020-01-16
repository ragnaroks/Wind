<template>
    <div class="components" data-component="daemon-information-panel">
        <p v-if="!currentDaemonItem" class="no-selected-notice">No selected any daemon</p>
        <CellGroup v-if="currentDaemonItem">
            <Cell v-bind:extra="currentDaemonItem.daemonVersion | daemonVersionFilter" title="Version" />
            <Cell v-bind:extra="currentDaemonItem.daemonWorkDirectory | daemonWorkDirectoryFilter" title="WorkDirectory" />
            <Cell v-bind:extra="currentDaemonItem.daemonHostCpuCores | daemonHostCpuCoresFilter" title="HostCpuCores" />
            <Cell v-bind:extra="currentDaemonItem.daemonHostMemorySize | fixedByteSizeFilter" title="HostMemorySize" />
            <Cell v-bind:extra="currentDaemonItem.daemonProcessId | daemonProcessIdFilter" title="ProcessId" />
            <Divider size="small" class="divider1" />
            <Cell title="AutoRefreshStatus">
                <i-switch slot="extra" v-model="refreshTimerEnabled" v-bind:disabled="!currentDaemonItem.websocketWrap.connectionValid" />
            </Cell>
            <Cell v-bind:extra="currentDaemonItem.daemonProcessTimePercentage | daemonProcessTimePercentageFilter" title="ProcessTimePercentage" />
            <Cell v-bind:extra="currentDaemonItem.daemonProcessWorkingSetSize | fixedByteSizeFilter" title="ProcessWorkingSetSize" />
            <Cell v-bind:extra="currentDaemonItem.daemonUnitSettingsCount | daemonUnitSettingsCountFilter" title="UnitSettingsCount" />
            <Cell v-bind:extra="currentDaemonItem.daemonUnitProcessCount | daemonUnitProcessCountFilter" title="UnitProcessCount" />
        </CellGroup>
    </div>
</template>

<style scoped>
.components .no-selected-notice{padding:1rem;}
.divider1{margin:1px 0;}
</style>

<script>
export default {
    filters:{
        fixedByteSizeFilter:function(value){
            if(!value){return 'unknown';}
            if(value>0 && value<1024){return value+' B';}
            if(value>=1024 && value<1048576){return (value/1024).toFixed(2).replace('.00','')+' KiB';}
            if(value>=1048576 && value<1073741824){return (value/1048576).toFixed(2).replace('.00','')+' MiB';}
            if(value>=1073741824 && value<109951162776){return (value/1073741824).toFixed(2).replace('.00','')+' GiB';}
            return (value/1073741824).toFixed(2).replace('.00','')+' GiB';
        },
        daemonVersionFilter:function(value){return !value?'unknown':value;},
        daemonWorkDirectoryFilter:function(value){return !value?'unknown':value;},
        daemonHostCpuCoresFilter:function(value){return !value?'unknown':value+'(logic)';},
        daemonProcessIdFilter:function(value){return !value?'unknown':value.toString();},
        daemonProcessTimePercentageFilter:function(value){
            return !value?'unknown':value.toFixed(2)+' %';
        },
        daemonUnitSettingsCountFilter:function(value){return (value===undefined||value===null)?'unknown':value.toString();},
        daemonUnitProcessCountFilter:function(value){return (value===undefined||value===null)?'unknown':value.toString();}
    },
    data:function(){
        return {
            refreshTimerEnabled:false,
            refreshTimerInterval:1000,//有需要可以自改
            refreshTimerId:0
        };
    },
    computed:{
        currentDaemonItem:function(){return this.$store.state.currentDaemonItem;}
    },
    created:function(){
        this.refreshTimerStart();
    },
    beforeDestroy:function(){
        this.refreshTimerStop();
    },
    methods:{
        refreshTimerStart:function(){
            const _this=this;
            this.refreshTimerId=window.setInterval(function(){
                if(!_this.refreshTimerEnabled){return;}
                //这里用 currentDaemonItem.websocketWrap 来自动限定关联的daemon
                if(!_this.currentDaemonItem || !_this.currentDaemonItem.websocketWrap){return;}
                _this.currentDaemonItem.websocketWrap.Invoke('instanceSendWebSocketClientRequestFetchDaemonStatus',null);
            },this.refreshTimerInterval);
        },
        refreshTimerStop:function(){
            window.clearInterval(this.refreshTimerId);
        },
        fixedByteSize:function(size){
            if(!size){return '0 B';}
            if(size>0 && size<1024){return size+' B';}
            if(size>=1024 && size<1048576){return (size/1024).toFixed(2)+' KiB';}
            if(size>=1048576 && size<1073741824){return (size/1048576).toFixed(2)+' MiB';}
            if(size>=1073741824 && size<109951162776){return (size/1073741824).toFixed(2)+' GiB';}
            return (size/1073741824).toFixed(2)+' GiB';
        }
    }
};
</script>
