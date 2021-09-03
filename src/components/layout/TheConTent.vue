<template>
    <div class="page" :class="{'active':check}">
        <router-view></router-view>
    </div>
</template>

<script>


export default {
    name : "TheConTent",
    components :{},
    props : {
         check : {
            type : Boolean,
            default : false,
            required : true
        },
    },
}
</script>

<style scoped>
@import url('../../assets/css/layout/Content.css');
</style>