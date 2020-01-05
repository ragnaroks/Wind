<template>
    <div class="pages" data-layout="default" data-page="index">
        <Sider v-bind:collapsedWidth="0" v-model="sider.isCollapsed" class="page-sider" breakpoint="sm" collapsible hide-trigger>
            <Menu v-on:on-select="onSiderMenuSelect" theme="dark" width="auto">
                <MenuItem name="menu-title" disabled><Icon type="md-list" />Wind2 daemon list</MenuItem>
                <MenuItem v-for="webSocketItem in webSocketArray" v-bind:key="webSocketItem.hostname" v-bind:name="webSocketItem.hostname" v-text="webSocketItem.hostname" />
                <MenuItem v-on:click.native="alert" name="menu-add-daemon" disabled><Icon type="md-add" />Add Daemon</MenuItem>
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
                        <component-websocket-item slot="content" v-bind:webSocketItem="currentActiveWebSocketItem" v-on:webSocketItemConnectionValidated="onWebSocketItemConnectionValidated" />
                    </Panel>
                    <Panel name="panel-for-daemon-controller" class="panel-for-daemon-controller">
                        daemon control
                        <component-daemon-controller slot="content" ref="component-daemon-controller" v-bind:webSocketItem="currentActiveWebSocketItem" />
                    </Panel>
                </Collapse>
            </Content>
        </Layout>
    </div>
</template>

<style scoped>
.pages{font-size:16px;/*height:fill-available;*/}
.page-sider{position:fixed;height:100vh;left:0;overflow:auto;}
.page-header{background-color:white;box-shadow:0 2px 3px 2px rgba(0,0,0,.1);font-weight:bold;position:fixed;top:0;width:100%;z-index:1;}
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
            webSocketArray:[
                {
                    hostname:'localhost',
                    instance:null,
                    connected:false,
                    address:'ws://127.0.0.1:25565',
                    controlKey:'https://github.com/ragnaroks/Wind2',
                    connectionId:null,
                    connectionValid:false,
                    recvivedText:'',
                    sentText:'',
                    recvivedLength:0,
                    sentLength:0
                }/*,{
                    hostname:'localhost[ANY]',
                    instance:null,
                    connected:false,
                    address:'ws://10.0.0.109:25565',
                    controlKey:'https://github.com/ragnaroks/Wind2',
                    connectionId:null,
                    connectionValid:false,
                    recvivedText:'',
                    sentText:'',
                    recvivedLength:0,
                    sentLength:0
                }*/
            ]
        };
    },
    computed:{
        currentActiveWebSocketItem:function(){
            for(let i1=0;i1<this.webSocketArray.length;i1++){
                if(this.webSocketArray[i1].hostname!==this.sider.menu.currentActiveName){continue;}
                return this.webSocketArray[i1];
            }
            return null;
        }
    },
    methods:{
        onSiderMenuSelect:function(name){this.sider.menu.currentActiveName=name;},
        onWebSocketItemConnectionValidated:function(webSocketItem){
            console.log(webSocketItem);
            if(this.$refs['component-daemon-controller']!==undefined && this.$refs['component-daemon-controller']!==null){
                this.$refs['component-daemon-controller'].daemonFetchAllUnitsSettings();
            }
        },
        alert:function(){
            this.$Message.info('nothing here');
        }
    }
};
</script>
