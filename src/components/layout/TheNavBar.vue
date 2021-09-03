<template>
    <div class="navbars" :class="{'active':check}">
        <!--  Left Content -->
            <div class="navbar__content">
                <router-link class="navbar__item" v-for="(value,index) in data" :key="index" 
                :to="data[index].to"
                >
                    <div class="icon" :class="data[index].icon"></div>
                    <div  class="navbar__name">{{data[index].value}}</div>
                </router-link>
            </div>
    </div>
</template>

<script>


export default {
    name : "TheNavbar",
    props : {
        check : {
            type : Boolean,
            default : false,
            required : true
        },
    },
    methods: {
    },
    data() {
        return {
            data : [
                {icon : 'icon-overview', value : 'Tổng quan', to : '/home'},
                {icon : 'icon-cash', value : 'Tiền mặt' , to : '/employee'},
                {icon : 'icon-deposits', value : 'Tiền gửi',to : '/1'},
                {icon : 'icon-buy', value : 'Mua hàng',to : '/2'},
                {icon : 'icon-sell', value : 'Bán hàng',to : '/3'},
                {icon : 'icon-manage', value : 'Quản lý hóa đơn',to : '/4'},
                {icon : 'icon-warehouse', value : 'Kho',to : '/5'},
                {icon : 'icon-tool', value : 'Công cụ dụng cụ',to : '/6'},
                {icon : 'icon-asset', value : 'Tài sản cố định',to : '/7'},
                {icon : 'icon-tax', value : 'Thuế',to : '/8'},
                {icon : 'icon-price', value : 'Giá thành',to : '/9'},
                {icon : 'icon-synthetic', value : 'Tổng hợp',to : '/10'},
                {icon : 'icon-budget', value : 'Ngân Sách',to : '/11'},
                {icon : 'icon-report', value : 'Báo cáo',to : '/12'},
                {icon : 'icon-finance', value : 'Phân tích tài chính',to : '/13'},
                
            ]
        }
    },
}
</script>

<style scoped>
@import url('../../assets/css/layout/Content.css');
</style>