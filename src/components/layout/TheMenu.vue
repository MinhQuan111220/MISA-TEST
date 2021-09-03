<template>
    <div class="header">
            <div class="header-left__logo" v-if="check == false">
                <div class="icon-box">
                </div>
                <div class="logo-box">
                    <img class="logo" src="../../assets/img/img/Logo_Module_TiengViet_White.66947422.svg" alt="">
                </div>
            </div>
            <div class="header-left__list" v-if="check == true">
                <div class="icon-list" @click="btnListClick(false)"></div>
            </div>
            <div class="content__header">
                    <div class="content__header-text">
                        <div class="icon-list" @click="btnListClick(true)" v-if="check == false"></div>
                        <div class="text-title">
                            <h3 class="title2">CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC </h3>
                            <i class="fas fa-chevron-down"></i>
                        </div>
                    </div>
                    <div class="content__header-accout">
                        <div class="account">
                            <div class="icon-bell"></div>
                            <img class="avatar" src="../../assets/icon/avatar-default.png" alt="">
                            <h3 class="title2 account-name">Nguyễn Văn Mạnh</h3>
                            <div class="icon-down"></div>
                        </div>
                    </div>
            </div>
    </div>
</template>

<script>
export default {
    name : "TheMenu",
    methods: {
        btnListClick(check){
            this.check = check
            this.$emit('showListIcon',check)
        }
    },
    data() {
        return {
            check : false,
        }
    },
}
</script>

<style scoped>
@import url('../../assets/css/layout/Menu.css');
</style>