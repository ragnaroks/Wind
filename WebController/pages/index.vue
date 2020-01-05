<template>
    <div class="pages" data-layout="default" data-page="index">
        <Sider v-bind:collapsedWidth="164" class="page-sider">
            <Menu v-on:on-select="onSiderMenuSelect" theme="dark" width="auto">
                <MenuItem name="menu-title" disabled><Icon type="md-list" />Wind2 daemon list</MenuItem>
                <MenuItem v-for="webSocketItem in webSocketArray" v-bind:key="webSocketItem.hostname" v-bind:name="webSocketItem.hostname" v-text="webSocketItem.hostname" />
                <MenuItem v-on:click.native="alert" name="menu-add-daemon" disabled><Icon type="md-add" />Add Daemon</MenuItem>
            </Menu>
        </Sider>
        <Layout class="page-body">
            <Header class="page-header">Wind2 Web Controller</Header>
            <Content class="page-content">
                <Collapse v-model="collapse.value">
                    <Panel name="panel-for-component-websocket-item">
                        daemon connection details
                        <component-websocket-item slot="content" v-bind:webSocketItem="currentActiveWebSocketItem" />
                    </Panel>
                    <Panel name="panel-for-daemon-controller">
                        daemon control
                    </Panel>
                </Collapse>
            </Content>
        </Layout>
    </div>
</template>

<style scoped>
.pages{font-size:16px;/*height:fill-available;*/}
.page-sider{position:fixed;height:100vh;left:0;overflow:auto;}
.page-header{background-color:white;box-shadow:0 2px 3px 2px rgba(0,0,0,.1);font-weight:bold;}
.page-body{margin-left:200px;background-color:white;}
.page-content{padding:0.5rem;}
</style>

<script>
import componentWebSocketItem from '@/components/WebSocketItem';
export default{
    components:{
        'component-websocket-item':componentWebSocketItem
    },
    data:function(){
        return {
            sider:{
                menu:{
                    currentActiveName:null
                }
            },
            collapse:{
                value:['panel-for-component-websocket-item']
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
                },{
                    hostname:'10.0.0.109',
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
                }
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
        alert:function(){window.alert('alert');},
        connectionOnMessage:function(event){
            console.log('链接收到消息',event);
            if(!event.data){return;}
            const args=event.data.split('§');
            const argsArray=[];
            for(let i1=0;i1<args.length;i1++){
                if(!args[i1]){continue;}
                argsArray.push(args[i1]);
            }
            if(argsArray.length<2){return;}//无效消息
            switch(argsArray[1]){
                case 'NotifySocketOpened':
                    this.webSocket.connectionId=argsArray[0];
                    this.webSocket.instance.send(this.webSocket.connectionId+'§CheckControlKey§'+this.webSocket.controlKey);
                break;
                case 'NotifyCheckControlKey':
                    if(argsArray[2]!=='success'){break;}
                    this.webSocket.connectionValid=true;
                break;
                case 'NotifyRefreshUnit':
                    console.log(argsArray[2]+'单元配置已刷新');
                break;
                case 'NotifyStartUnit':
                    console.log(argsArray[2]+'单元配置已启动');
                break;
                case 'NotifyStopUnit':
                    console.log(argsArray[2]+'单元配置已停止');
                break;
                default:
                    //
                break;
            }
        }
    }
};
</script>
