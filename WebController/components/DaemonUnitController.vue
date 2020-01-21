<template>
    <div class="components" data-component="daemon-controller">
        <p v-if="!currentDaemonItem" class="no-selected-notice">{{ localLanguageText['10007'] }}</p>
        <p v-else-if="!currentDaemonItem.websocketWrap || !currentDaemonItem.websocketWrap.connected" class="no-selected-notice">{{ localLanguageText['10008'] }}</p>
        <p v-else-if="currentDaemonItem.websocketWrap.connected && !currentDaemonItem.websocketWrap.connectionValid" class="no-selected-notice">{{ localLanguageText['10009'] }}</p>
        <div v-else class="daemon-unit-list">
            <Row v-if="currentDaemonItem.unitsStatusArray.length>0" v-bind:gutter="8" type="flex" align="top" class="daemon-unit-list-row">
                <i-col v-bind:xs="24" v-bind:md="12" v-bind:lg="8" v-bind:xxl="6"
                v-for="unitStatusItem in currentDaemonItem.unitsStatusArray" v-bind:key="unitStatusItem.unitName" class-name="daemon-unit-item">
                    <component-daemon-unit-item v-bind:unitStatusItem="unitStatusItem" v-bind:daemonItem="currentDaemonItem" />
                </i-col>
            </Row>
        </div>
    </div>
</template>

<style scoped>
.components .no-selected-notice{padding:1rem;}
.daemon-unit-list-row{padding:0.5rem;}
.daemon-unit-item{margin-bottom:0.5rem;}
</style>

<script>
import componentDaemonUnitItem from '@/components/DaemonUnitItem';

export default {
    components:{
        'component-daemon-unit-item':componentDaemonUnitItem
    },
    data:function(){
        return {
            actionUnitName:null
        };
    },
    computed:{
        localLanguageText:function(){return this.$store.getters.get_localLanguageText;},
        currentDaemonItem:function(){return this.$store.state.currentDaemonItem;}
    },
    methods:{
        setActionUnitName:function(unitName){this.actionUnitName=unitName;}
    }
};
</script>
