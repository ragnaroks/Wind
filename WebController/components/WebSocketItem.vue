<template>
    <div class="components" data-component="web-socket-item">
        <p v-if="!webSocketItem" class="no-selected-notice">No selected any daemon</p>
        <CellGroup v-if="!!webSocketItem">
            <Cell v-bind:extra="webSocketItem.hostname" title="Hostname" />
            <Cell title="IsConnect">
                <i-switch slot="extra" v-model="webSocketItem.connected" v-bind:before-change="beforeChangeWebSocketItemConnected" />
            </Cell>
            <Cell v-bind:extra="webSocketItem.address" title="Address" />
            <Cell v-bind:extra="webSocketItem.controlKey" title="ControlKey" />
            <Cell v-bind:extra="webSocketItem.connectionId" title="ConnectionId" />
            <Cell title="IsConnectionValidated">
                <i-switch slot="extra" v-model="webSocketItem.connectionValid" disabled />
            </Cell>
        </CellGroup>
    </div>
</template>

<style scoped>
.components .no-selected-notice{padding:1rem;}
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
            splitChar:'§'
        };
    },
    methods:{
        beforeChangeWebSocketItemConnected:function(){
            if(this.webSocketItem.connected){
                this.webSocketItem.instance.close(1000,'user-close');
                this.webSocketItem.connected=false;
            }else{
                try{
                    this.webSocketItem.instance=new WebSocket(this.webSocketItem.address);
                }catch(exception){
                    console.error(exception);
                    this.$Notice.error({
                        title:this.webSocketItem.hostname,
                        desc:this.webSocketItem.hostname+' can not connect (see details in devtool)'
                    });
                }
                const _this=this;
                this.webSocketItem.instance.onopen=function(onopenEvent){_this.webSocketItemOnOpen(onopenEvent);};
                this.webSocketItem.instance.onerror=function(onerrorEvent){_this.webSocketItemOnError(onerrorEvent);};
                this.webSocketItem.instance.onclose=function(oncloseEvent){_this.webSocketItemOnClose(oncloseEvent);};
                this.webSocketItem.instance.onmessage=function(onmessageEvent){_this.webSocketItemOnMessage(onmessageEvent);};
            }
        },
        webSocketItemOnOpen:function(onopenEvent){
            console.log(onopenEvent);
            this.webSocketItem.connected=true;
            this.$Notice.open({
                title:this.webSocketItem.hostname,
                desc:this.webSocketItem.hostname+' has been connected'
            });
        },
        webSocketItemOnError:function(onerrorEvent){
            console.error(onerrorEvent);
            this.webSocketItem.instance.close(1000,'error-close');
            this.$Notice.error({
                title:this.webSocketItem.hostname,
                desc:this.webSocketItem.hostname+' has been disconnected because an error (see details in devtool)'
            });
        },
        webSocketItemOnClose:function(oncloseEvent){
            console.log(oncloseEvent);
            this.webSocketItem.connected=false;
            this.webSocketItem.connectionId=null;
            this.webSocketItem.connectionValid=false;
            this.$Notice.info({
                title:this.webSocketItem.hostname,
                desc:this.webSocketItem.hostname+' has been disconnected'
            });
        },
        webSocketItemOnMessage:function(onmessageEvent){
            console.log(onmessageEvent);
            if(onmessageEvent.data===null || onmessageEvent.data.length<1){return;}
            this.webSocketItem.recvivedText+=onmessageEvent.data+'\n';
            this.webSocketItem.recvivedLength+=onmessageEvent.data.length;
            const dataArray=onmessageEvent.data.split(this.splitChar);
            const argsArray=[];
            for(let i1=0;i1<dataArray.length;i1++){
                if(!dataArray[i1]){continue;}
                argsArray.push(dataArray[i1]);
            }
            if(argsArray.length<2){return;}
            switch(argsArray[1]){
                case 'NotifySocketOpened':this.webSocketItemOnMessage_NotifySocketOpened(argsArray);break;
                case 'NotifyCheckControlKey':this.webSocketItemOnMessage_NotifyCheckControlKey(argsArray);break;
                case 'NotifyRefreshUnit':this.webSocketItemOnMessage_NotifyRefreshUnit(argsArray);break;
                case 'NotifyStartUnit':this.webSocketItemOnMessage_NotifyStartUnit(argsArray);break;
                case 'NotifyStopUnit':this.webSocketItemOnMessage_NotifyStopUnit(argsArray);break;
                default:break;
            }
        },
        webSocketItemOnMessage_NotifySocketOpened:function(argsArray){
            this.webSocketItem.connectionId=argsArray[0];
            this.webSocketItem.instance.send(this.webSocketItem.connectionId+this.splitChar+'CheckControlKey'+this.splitChar+this.webSocketItem.controlKey);
        },
        webSocketItemOnMessage_NotifyCheckControlKey:function(argsArray){
            if(argsArray[2]!=='success'){
                this.webSocketItem.close(1000,'invalid control key');
                this.$Notice.warning({
                    title:this.webSocketItem.hostname,
                    desc:this.webSocketItem.hostname+' has been disconnected because invalid control key'
                });
                return;
            }
            this.webSocketItem.connectionValid=true;
            this.$emit('webSocketItemConnectionValidated',this.webSocketItem);
            this.$Notice.success({
                title:this.webSocketItem.hostname,
                desc:this.webSocketItem.hostname+' has been validated this connection'
            });
        },
        webSocketItemOnMessage_NotifyRefreshUnit:function(argsArray){
            if(!argsArray[2]){return;}
            this.$Notice.info({
                title:this.webSocketItem.hostname,
                desc:this.webSocketItem.hostname+' unit ['+argsArray[2]+'] has been refresh'
            });
        },
        webSocketItemOnMessage_NotifyStartUnit:function(argsArray){
            if(!argsArray[2]){return;}
            this.$Notice.info({
                title:this.webSocketItem.hostname,
                desc:this.webSocketItem.hostname+' unit ['+argsArray[2]+'] has been started'
            });
        },
        webSocketItemOnMessage_NotifyStopUnit:function(argsArray){
            if(!argsArray[2]){return;}
            this.$Notice.info({
                title:this.webSocketItem.hostname,
                desc:this.webSocketItem.hostname+' unit ['+argsArray[2]+'] has been stopped'
            });
        }
    }
};
</script>
