<template>
        <div class="toast-messenger" :class="{'active':isShowToast}">
           <div class="toast-messenger__show">
                <div class="toast-icon" :class="{'toast-success':isSuccessToastMessage,'toast-warning':isWarningToastMessage,'toast-error':isErrorToastMessage}">
                <i class="fas fa-check-circle" :class="{'toast-success':isSuccessToastMessage}"></i>
                <i class="fas fa-exclamation-circle" :class="{'toast-error':isErrorToastMessage}"></i>
                <i class="fas fa-exclamation-triangle" :class="{'toast-warning':isWarningToastMessage}"></i>
                </div>    
                <div class="toast-title">
                    {{lable}}
                </div>
                    <img
                    src="../../assets/icon/x.svg"
                    alt=""
                    class="toast-close"
                    :class="{'toast-success':isSuccessToastMessage,'toast-warning':isWarningToastMessage,'toast-error':isErrorToastMessage}"
                    @click="btnExitOnClick"
                    />
           </div>
        </div>
</template>

<script>
export default {
    name : 'ToastMessenger',
    props:{

        isShowToast: {
            type: Boolean,
            default : true,
            required: true,
        },
        isSuccessToastMessage: {
            type: Boolean,
            default : false,
            required: true,
        },
        isWarningToastMessage: {
            type: Boolean,
            default : false,
            required: true,
        },
        isErrorToastMessage: {
            type: Boolean,
            default : false,
            required: true,
        },
        lable : {
            type : String,
            required : 'true',
        }
    },
    methods: {
        /**
         * Ẩn toast
         */
        btnExitOnClick(){
            this.isShowToast = true
        }
    },
}   
</script>

<style >
   @import url('../../assets/css/base/ToastMessenger.css');
</style>