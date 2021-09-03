<template>
  <div id="main">
    <TheMenu
     @showListIcon="showListIcon" 
    />
    <TheNavbar 
    :check="check"
    />
    <TheConTent 
    :check="check"
    />
  </div>
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue'
import TheConTent from './components/layout/TheConTent.vue'
import TheMenu from './components/layout/TheMenu.vue'
import TheNavbar from './components/layout/TheNavBar.vue'
export default {
  name: 'Main',
  components: {
    TheConTent,
    TheMenu,
    TheNavbar
  },
  methods: {
    showListIcon(check){
      this.check = check
    }
  },
  data() {
    return {
      check : false
    }
  },
}
</script>

<style>

@import url('../src/assets/css/base/Base.css');
</style>
