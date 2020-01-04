<template>
    <div class="pages" data-layout="default" data-page="index">
        <Sider v-bind:collapsedWidth="164" class="page-sider">
            <Menu v-on:on-select="onSiderMenuSelect" theme="dark" width="auto">
                <MenuItem name="menu-title" disabled><Icon type="md-document" />服务主机列表</MenuItem>
                <MenuItem v-for="webSocketItem in webSocketArray" v-bind:key="webSocketItem.name" v-bind:name="webSocketItem.name" v-text="webSocketItem.name" />
            </Menu>
        </Sider>
        <Layout class="page-body">
            <Header class="page-header">Wind2 Web Controller</Header>
            <Content class="page-content">
                选择了 {{ sider.menu.currentActiveName }} 服务主机
            </Content>
        </Layout>
        <!--
        <Header class="layout-header">Wind2 Web Controller</Header>
        <Layout>
            <Sider hide-trigger class="layout-sider">
                <p>服务主机列表</p>
                <i-button shape="circle" icon="ios-search" />
            </Sider>
            <Content class="layout-content">
                <div class="e1">
                    <label>服务主机地址: <input v-model="webSocket.address" v-bind:disabled="webSocket.connected" type="text" placeholder="ws://127.0.0.1:25565"></label>
                    <label>远程控制密钥: <input v-model="webSocket.controlKey" v-bind:disabled="webSocket.connected" type="text" placeholder="https://github.com/ragnaroks/Wind2"></label>
                    <button v-bind:disabled="webSocket.connected" v-on:click="connectToDaemon">链接</button>
                    <button v-bind:disabled="!webSocket.connected" v-on:click="connectionClose">断开</button>
                    <label>消息: <input v-model="messageText" v-bind:disabled="!webSocket.connected" type="text"></label>
                    <button v-bind:disabled="!webSocket.connected" v-on:click="connectionSendMessage">发送</button>
                </div>
            </Content>
        </Layout>
        -->
    </div>
</template>

<style scoped>
.pages{font-size:16px;/*height:fill-available;*/}
.page-sider{position:fixed;height:100vh;left:0;overflow:auto;}
.page-header{background-color:white;box-shadow:0 2px 3px 2px rgba(0,0,0,.1);}
.page-body{margin-left:200px;background-color:white;}
.page-content{padding:0.5rem;}
</style>

<script>
export default{
    data:function(){
        return {
            sider:{
                menu:{
                    currentActiveName:null
                }
            },
            webSocketArray:[
                {
                    name:'localhost',
                    instance:null,
                    connected:false,
                    address:'ws://127.0.0.1:25565',
                    controlKey:'https://github.com/ragnaroks/Wind2',
                    connectionId:null,
                    connectionValid:false
                }
            ]
        };
    },
    methods:{
        onSiderMenuSelect:function(name){
            this.sider.menu.currentActiveName=name;
        },
        //
        connectToDaemon:function(event){
            if(!this.webSocket.address){return;}
            try{
                this.webSocket.instance=new WebSocket(this.webSocket.address);
            }catch(exception){
                console.log('链接时异常',exception);
                return;
            }
            const _this=this;
            this.webSocket.instance.onopen=function(onopenEvent){_this.connectionOnOpen(onopenEvent);};
            this.webSocket.instance.onerror=function(onerrorEvent){_this.connectionOnError(onerrorEvent);};
            this.webSocket.instance.onclose=function(oncloseEvent){_this.connectionOnClose(oncloseEvent);};
            this.webSocket.instance.onmessage=function(onmessageEvent){_this.connectionOnMessage(onmessageEvent);};
        },
        connectionOnOpen:function(event){
            console.log('链接已打开',event);
            this.webSocket.connected=true;
        },
        connectionOnError:function(event){
            console.log('链接异常',event);
        },
        connectionOnClose:function(event){
            console.log('链接关闭',event);
            this.webSocket.connected=false;
        },
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
        },
        connectionSendMessage:function(){
            this.webSocket.instance.send(this.messageText);
        },
        connectionClose:function(){
            this.webSocket.instance.close();
            this.webSocket.connected=false;
        }
    }
};
</script>
