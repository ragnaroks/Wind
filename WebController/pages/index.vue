<template>
    <div class="pages" data-layout="default" data-page="index">
        <Sider v-bind:collapsedWidth="0" v-model="sider.isCollapsed" class="page-sider" breakpoint="sm" collapsible hide-trigger>
            <Menu v-on:on-select="onSiderMenuSelect" theme="dark" width="auto">
                <MenuItem name="menu-title" disabled><Icon type="md-list" />Wind2 daemon list</MenuItem>
                <MenuItem v-for="webSocketArrayHostname in webSocketArrayHostnameArray" v-bind:key="webSocketArrayHostname" v-bind:name="webSocketArrayHostname" v-text="webSocketArrayHostname" />
                <MenuItem v-on:click.native="showAddDaemonDialog" name="menu-add-daemon" disabled><Icon type="md-add" />Add Daemon</MenuItem>
            </Menu>
        </Sider>
        <Layout v-bind:class="['page-body',sider.isCollapsed?'page-sider-collapsed':'']">
            <Header class="page-header">
                <Icon v-on:click="sider.isCollapsed=!sider.isCollapsed" type="md-menu" style="font-size:1.6rem;margin-right:1rem;" />
                <span>Wind2 Web Controller</span>
            </Header>
            <Content class="page-content">
                <Collapse v-model="collapse.value">
                    <Panel name="panel-for-component-websocket-item">
                        daemon connection details
                        <component-websocket-item slot="content" v-on:webSocketItemConnectionValidated="onWebSocketItemConnectionValidated" />
                    </Panel>
                    <Panel name="panel-for-daemon-controller" class="panel-for-daemon-controller">
                        daemon control
                        <component-daemon-controller slot="content" ref="component-daemon-controller" />
                    </Panel>
                </Collapse>
            </Content>
            <Modal v-model="modalArray.showAddDaemonModal" v-bind:closable="false" v-bind:mask-closable="false" v-on:on-ok="AddDaemon" loading ok-text="Add" cancel-text="Cancel" title="Add Daemon">
                <div>AddDaemonModal</div>
            </Modal>
        </Layout>
    </div>
</template>

<style scoped>
.pages{font-size:16px;/*height:fill-available;*/}
.page-sider{position:fixed;height:100vh;left:0;overflow:auto;}
.page-header{background-color:white;box-shadow:0 2px 3px 2px rgba(0,0,0,.1);font-weight:bold;position:fixed;top:0;width:100%;z-index:1;padding-left:1rem;}
.page-body{margin-left:200px;background-color:white;}
.page-body.page-sider-collapsed{margin-left:0;}
.page-content{padding:0.5rem;margin-top:4rem;}
</style>
<style>
.panel-for-daemon-controller .ivu-collapse-content{background-color:#f0f0f0;}
</style>

<script>
import componentWebSocketItem from '@/components/WebSocketItem';
import componentDaemonController from '@/components/DaemonController';

export default{
    components:{
        'component-websocket-item':componentWebSocketItem,
        'component-daemon-controller':componentDaemonController
    },
    data:function(){
        return {
            sider:{
                isCollapsed:false,
                menu:{
                    currentActiveName:null
                }
            },
            collapse:{
                value:['panel-for-component-websocket-item','panel-for-daemon-controller']
            },
            modalArray:{
                showAddDaemonModal:false
            }
        };
    },
    computed:{
        currentWebSocketItemHostname:{
            get:function(){
                return this.$store.state.currentWebSocketItemHostname;
            },
            set:function(value){
                this.$store.commit('setCurrentWebSocketItemHostname',{hostname:value});
            }
        },
        currentWebSocketItem:function(){
            if(this.$store.state.webSocketArray.length<1){return null;}
            for(let i1=0;i1<this.$store.state.webSocketArray.length;i1++){
                if(this.$store.state.webSocketArray[i1].hostname!==this.$store.state.currentWebSocketItemHostname){continue;}
                return this.$store.state.webSocketArray[i1];
            }
            return null;
        },
        webSocketArrayHostnameArray:function(){
            const array=this.$store.state.webSocketArray;
            if(array.length<1){return null;}
            const hostnameArrya=[];
            for(let i1=0;i1<array.length;i1++){
                hostnameArrya.push(array[i1].hostname);
            }
            return hostnameArrya;
        }
    },
    methods:{
        onSiderMenuSelect:function(name){
            this.sider.menu.currentActiveName=name;
            this.currentWebSocketItemHostname=name;
        },
        onWebSocketItemConnectionValidated:function(webSocketItemHostname){
            if(this.$refs['component-daemon-controller']!==undefined && this.$refs['component-daemon-controller']!==null){
                this.$refs['component-daemon-controller'].daemonFetchAllUnits();
            }
        },
        showAddDaemonDialog:function(){
            if(this.modalArray.showAddDaemonModal){return;}
            this.modalArray.showAddDaemonModal=true;
        },
        AddDaemon:function(){
            const _this=this;
            setTimeout(() => {
                _this.modalArray.showAddDaemonModal=false;
            }, 1500);
        }
    }
};
</script>
