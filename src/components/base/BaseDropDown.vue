<template>
    <div class="dropdown"  :id="'dropdown'+name" @click="selectItem" :class="{'focus':isFocus}">
            <input type="text" class="dropdown-input active" :class="'dropdown-input'+name" v-model="nameShow"  readonly @keydown="keyDown" @click="inputClick">
            <div class="dropdown-icon" @click="inputClick">
                <div class="icon-down" :class="{'active':isHideIconUp}"></div>
                <div class="icon-up" :class="{'active':isHideIconDown}"></div>
            </div>
            <ul class="dropdown-list" :class="{'active':isHide,'last':isLast}">
                <li :id="'dropdown'+id" class="dropdown-list__item" 
                v-for="(item,index) in options" :key="item[id]" 
                v-on:click="showItemList(item[name],index)"
                :class="index == itemIndex ? 'dropdown-list__item-focus' :'' " 
                >
                    <span>
                        {{item[name]}}
                    </span>
                </li>
            </ul>
        </div>
</template>


<script>
import $ from 'jquery'



export default {
    name : "DropDown",
    props : {
        name: {
            type: String,
            requied: true,
        },
        id: {
            type: String,
            requied: true,
        },
        options : {
            required: true,
            default :() =>{},
            type: [Object, Array]
        },
        isLast : {
            type : Boolean,
            default : false,
        },
      
    },
    methods: {
            /**
             * Xử lý ẩn hiện list-dropdown
             * createdBy : PVM.Quân(31/08/2021)
             */
            selectItem(){
                // Gọi đến sự kiện của input
                this.inputClick()
            },

            /**
             * Bắt sự kiển click của input
             * createdBy : PVM.Quân(31/08/2021)
             */
            inputClick(){
                // Hiện list-dropdown
                var _this = this
                this.isHide = true
                this.isHideIconUp = true
                this.isHideIconDown = false
                this.isFocus = true

                /**
                  * Xử lý sự kiện ẩn hiện list-dropdown khi click ra ngoài 
                  * createdBy : PVM.Quân(31/08/2021)
                */
                   $(window).on("click.Bst", function(event){
                    if($(`#${'dropdown'+_this.name}`).has(event.target).length == 0 && !$(`#${'dropdown'+_this.name}`).is(event.target)){
                        // Nếu khác list-dropdown thì ẩn
                        _this.isHide = false
                        _this.isHideIconUp = false
                        _this.isHideIconDown = true
                        _this.isFocus = false
                    }else{
                        // Nếu trùng thì hiện
                        _this.isHide = true
                        _this.isHideIconUp = true
                        _this.isHideIconDown = false
                        _this.isFocus = true
                    }
                })
            },

            /**
             * Khi click vào từng thành phần của list-dropdown
             * @param {Tên các thành phần} nameShow
             * @param {Vị trí các thành phần} index
             * createdBy : PVM.Quân(31/08/2021)
             */
            showItemList(nameShow,index){
                // Gán tên và vị trí tương ứng để hiển thị
                this.nameShow = nameShow
                this.itemIndex = index

                // Ẩn list-dropdown
                setTimeout(()=>{
                  this.isHide = false
                  this.isHideIconUp = false
                  this.isHideIconDown = true
                  this.isFocus = false
                },10)
            },

            /**
             * Xử lý sự kiện keydown của ô input
             * createdBy : PVM.Quân(31/08/2021)
             */
            keyDown(event){
                var _this = this 
                // Khi down
                if(event.keyCode == 40){
                    this.itemIndex++
                  if(this.itemIndex > this.options.length-1){
                    this.itemIndex = 0
                  }
                }
                // Khi up
                if(event.keyCode == 38){
                    this.itemIndex--
                  if(this.itemIndex <0){
                    this.itemIndex = this.options.length-1
                  }
                }
                // Khi Enter
                if(event.keyCode == 13){
                    // Hiển thị giá trị lấy được ra input
                  this.nameShow = this.options[_this.itemIndex][_this.name]
                  
                  // Ẩn list-dropdown
                  setTimeout(()=>{
                    this.isHide = false
                    this.isHideIconUp = false
                    this.isHideIconDown = true
                    this.isFocus = false
                  },100)
                }
            },
            
        },

    data() {
            return {
                isHide : false,
                isHideIconUp : false,
                isHideIconDown : true,
                isFocus : false,
                nameShow : this.options[0][this.name],
                idRequest : '',
                employee : {},
                index : 0,
                itemIndex : 0,
            }
    },

    watch :{
         /**
         * Truyền dữ liệu lên cha 
         * PVM.Quân (04/08/2021)
         */
        nameShow : function(){
            var _this = this
            this.$emit('sendValueDropDown',this.options[_this.itemIndex][_this.id])
        },
        
    }

}




// author : PVM.Quân
</script>

<style scoped>
@import url('../../assets/css/base/Dropdown.css');
@import url('../../assets/css/base/Input.css');

</style>