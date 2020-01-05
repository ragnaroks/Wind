<template>
    <div class="components" data-component="web-socket-item">
        <p v-if="!webSocketItem">No selected any daemon</p>
        <CellGroup v-if="!!webSocketItem">
            <Cell v-bind:extra="webSocketItem.hostname" title="Hostname" />
            <Cell title="Connecte">
                <i-switch slot="extra" v-model="webSocketItem.connected" v-bind:before-change="beforeChangeWebSocketItemConnected" />
            </Cell>
            <Cell v-bind:extra="webSocketItem.address" title="Address" />
            <Cell v-bind:extra="webSocketItem.controlKey" title="ControlKey" />
            <Cell v-bind:extra="webSocketItem.connectionId" title="ConnectionId" />
            <Cell title="ConnectionValid">
                <i-switch slot="extra" v-model="webSocketItem.connectionValid" disabled />
            </Cell>
        </CellGroup>
    </div>
</template>

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
            switchValue:false
        };
    },
    methods:{
        beforeChangeWebSocketItemConnected:function(){
            if(this.webSocketItem.connected){
                this.webSocketItem.instance.close(1000,'user-close');
                this.webSocketItem.connected=false;
            }else{
                this.webSocketItem.instance=new WebSocket(this.webSocketItem.address);
                const _this=this;
                this.webSocketItem.instance.onopen=function(onopenEvent){
                    console.log(onopenEvent);
                    _this.webSocketItem.connected=true;
                    _this.$Notice.open({
                        title:_this.webSocketItem.hostname,
                        desc:_this.webSocketItem.hostname+' has been connected'
                    });
                };
                this.webSocketItem.instance.onerror=function(onerrorEvent){
                    console.log(onerrorEvent);
                    _this.webSocketItem.instance.close(1011,'error-close');
                    _this.$Notice.open({
                        title:_this.webSocketItem.hostname,
                        desc:_this.webSocketItem.hostname+' has been disconnected because error'
                    });
                };
                this.webSocketItem.instance.onclose=function(oncloseEvent){
                    console.log(oncloseEvent);
                    _this.webSocketItem.connected=false;
                    _this.$Notice.open({
                        title:_this.webSocketItem.hostname,
                        desc:_this.webSocketItem.hostname+' has been disconnected'
                    });
                };
                this.webSocketItem.instance.onmessage=function(onmessageEvent){
                    console.log(onmessageEvent);
                    _this.webSocketItem.recvivedText+=onmessageEvent.data+'\n';
                    _this.webSocketItem.recvivedLength+=onmessageEvent.data.length;
                };
            }
        }
    }
};
</script>
