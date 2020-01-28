<template>
    <div class="pages" data-layout="default" data-page="index">
        <Sider v-bind:collapsedWidth="0" v-model="sider.isCollapsed" class="page-sider" breakpoint="sm" collapsible hide-trigger>
            <Menu v-on:on-select="onMenuSelect" theme="dark" width="auto">
                <MenuItem v-on:click.native="showChangeLanguageDialog" name="menu-change-language" disabled><Icon type="md-globe" />{{ localLanguageText['10049'] }}</MenuItem>
                <MenuItem v-if="false" v-on:click.native="showAddDaemonDialog" name="menu-add-daemon" disabled><Icon type="md-add" />{{ localLanguageText['10003'] }}</MenuItem>
                <Divider size="small" class="divider1" />
                <MenuItem name="menu-title" disabled><Icon type="md-list" />{{ localLanguageText['10002'] }}</MenuItem>
                <MenuItem v-for="daemonHostnameItem in daemonHostnameArray" v-bind:key="daemonHostnameItem" v-bind:name="daemonHostnameItem" v-text="daemonHostnameItem" />
            </Menu>
        </Sider>
        <Layout v-bind:class="['page-body',sider.isCollapsed?'page-sider-collapsed':'']">
            <Header class="page-header">
                <Icon v-on:click="sider.isCollapsed=!sider.isCollapsed" type="md-menu" style="font-size:1.6rem;margin-right:1rem;" />
                <span>Wind2 {{ localLanguageText['10001'] }}</span>
            </Header>
            <Content class="page-content">
                <Collapse v-model="collapse.value">
                    <Panel name="panel-for-component-daemon-connection-panel">
                        <span>{{ localLanguageText['10004'] }}</span>
                        <component-daemon-connection-panel slot="content" />
                    </Panel>
                    <Panel name="panel-for-component-daemon-information-panel">
                        <span>{{ localLanguageText['10005'] }}</span>
                        <component-daemon-information-panel slot="content" />
                    </Panel>
                    <Panel name="panel-for-daemon-unit-controller" class="panel-for-daemon-controller">
                        <span>{{ localLanguageText['10006'] }}</span>
                        <component-daemon-unit-controller slot="content" ref="component-daemon-unit-controller" />
                    </Panel>
                </Collapse>
            </Content>
            <Modal v-model="modalArray.showChangeLanguageModal" v-bind:mask-closable="false" v-bind:title="localLanguageText['10049']" footer-hide>
                <CellGroup v-on:on-click="changeLanguageTextType">
                    <Cell v-bind:selected="currentLanguageTextType==='zh-cn'" title="简体中文" extra="zh-cn" name="zh-cn" />
                    <Cell v-bind:selected="currentLanguageTextType==='en-us'" title="English" extra="en-us" name="en-us" />
                </CellGroup>
            </Modal>
            <Modal v-model="modalArray.showAddDaemonModal" v-bind:closable="false" v-bind:mask-closable="false" v-on:on-ok="addDaemon" loading ok-text="Add" cancel-text="Cancel" title="Add Daemon">
                <div>AddDaemonModal</div>
            </Modal>
        </Layout>
    </div>
</template>

<style scoped>
.pages{font-size:16px;/*height:fill-available;*/}
.page-sider{position:fixed;height:100vh;left:0;overflow:auto;}
.divider1{margin:0;display:inline-block;background-color:#424752;}
.page-header{background-color:white;box-shadow:0 2px 3px 2px rgba(0,0,0,.1);font-weight:bold;position:fixed;top:0;width:100%;z-index:1;padding-left:1rem;}
.page-body{margin-left:200px;background-color:white;}
.page-body.page-sider-collapsed{margin-left:0}
.page-body.page-sider-collapsed .page-header{background-color:#515a6e;color:white;}
.page-content{padding:0.5rem;margin-top:4rem;}
</style>
<style>
.panel-for-daemon-controller .ivu-collapse-content{background-color:#f0f0f0;}
</style>

<script>
import componentDaemonConnectionPanel from '@/components/DaemonConnectionPanel';
import componentDaemonInformationPanel from '@/components/DaemonInformationPanel';
import componentDaemonUnitController from '@/components/DaemonUnitController';

export default{
    components:{
        'component-daemon-connection-panel':componentDaemonConnectionPanel,
        'component-daemon-information-panel':componentDaemonInformationPanel,
        'component-daemon-unit-controller':componentDaemonUnitController
    },
    data:function(){
        return {
            sider:{
                isCollapsed:false
            },
            collapse:{
                value:['panel-for-component-daemon-connection-panel','panel-for-component-daemon-information-panel','panel-for-daemon-unit-controller']
            },
            modalArray:{
                showAddDaemonModal:false,
                showChangeLanguageModal:false
            }
        };
    },
    computed:{
        localLanguageText:function(){return this.$store.getters.get_localLanguageText;},
        currentDaemonHostname:{
            get:function(){
                return this.$store.state.currentDaemonHostname;
            },
            set:function(value){
                this.$store.commit('set_currentDaemonHostname',{hostname:value});
            }
        },
        currentLanguageTextType:{
            get:function(){
                return this.$store.state.languageTextType;
            },
            set:function(value){
                this.$store.commit('set_languageTextType',{languageTextType:value});
            }
        },
        daemonHostnameArray:function(){
            const array=this.$store.state.daemonArray;
            if(array.length<1){return null;}
            const hostnameArray=[];
            for(let i1=0;i1<array.length;i1++){
                hostnameArray.push(array[i1].hostname);
            }
            return hostnameArray;
        }
    },
    created:function(){
        const languageTextType=localStorage.getItem('languageTextType');
        if(!languageTextType){
            this.currentLanguageTextType='zh-cn';
        }else{
            this.currentLanguageTextType=languageTextType;
        }
    },
    methods:{
        onMenuSelect:function(name){
            this.currentDaemonHostname=name;
        },
        onLanguageTypeChange:function(name){
            console.log(name);
            this.currentLanguageTextType=name;
        },
        showChangeLanguageDialog:function(){
            if(this.modalArray.showChangeLanguageModal){return;}
            this.modalArray.showChangeLanguageModal=true;
        },
        showAddDaemonDialog:function(){
            if(this.modalArray.showAddDaemonModal){return;}
            this.modalArray.showAddDaemonModal=true;
        },
        changeLanguageTextType:function(type){
            this.currentLanguageTextType=type;
        },
        addDaemon:function(){
            const _this=this;
            setTimeout(() => {
                _this.modalArray.showAddDaemonModal=false;
            }, 1500);
        }
    }
};
</script>
