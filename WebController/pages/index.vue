<template>
    <div class="pages" data-layout="default" data-page="index">
        <div class="page-body">
            <div class="e1">
                <label>服务主机地址: <input v-model="webSocket.address" v-bind:disabled="webSocket.connected" type="text" placeholder="ws://127.0.0.1:25565"></label>
                <button v-bind:disabled="webSocket.connected" v-on:click="connectToDaemon">链接</button>
                <button v-bind:disabled="!webSocket.connected" v-on:click="connectionClose">断开</button>
                <label>消息: <input v-model="messageText" v-bind:disabled="!webSocket.connected" type="text"></label>
                <button v-bind:disabled="!webSocket.connected" v-on:click="connectionSendMessage">发送</button>
            </div>
        </div>
    </div>
</template>

<style scoped>
.page-body{padding:0.5rem;}
</style>

<script>
export default{
    data:function(){
        return {
            webSocket:{
                instance:null,
                connected:false,
                address:'ws://127.0.0.1:25565'
            },
            messageText:null
        };
    },
    methods:{
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
            window.temp=event;
            console.log('链接收到消息',event);
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
