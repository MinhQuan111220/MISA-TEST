<template>
    <div class="combobox"  :id="'combobox'+name" @click="selectItem" :class="{'focus':isFocus,'invalid':checkValidate}" :title="errorValidate">
            <input type="text" class="combobox-input active"  
            :class="'combobox-input'+name" 
            v-model="nameShow" 
            placeholder="" 
            :ref="'combobox-input'+name"
            @input="onInput"
            @keydown="keyDown"
            @blur="onBlur"
            >
            <div class="combobox-icon" >
                <div class="icon-down"></div>
            </div>
            <ul class="combobox-list" :class="{'active':isHide}">
                <li class="combobox-list__item combobox-list__item-first">
                    <span class="item-left">Mã đơn vị</span>
                    <span class="item-rigth">
                        Tên đơn vị
                    </span>
                </li>
                <li  class="combobox-list__item" v-for="(item,index) in dataFilter" :key="item[id]" 
                v-on:click="showItemListCBB(item[name],index+1)"
                :class="index +1 == itemIndex ? 'combobox-list__item-focus' : ''"
                >
                    <span class="item-left">{{item[code]}}</span>
                    <span class="item-rigth">
                        {{item[name].toUpperCase()}}
                    </span>
                </li>
            </ul>
        </div>
</template>


<script>
import $ from 'jquery'
export default {
    name : "ComBoBox",
    props : {
        values: {
            type: String,
            default : '',
            requied: true,
        },
        label: {
            type: String,
            requied: true,
        },
        name: {
            type: String,
            requied: true,
        },
        code: {
            type: String,
            requied: true,
        },
        id: {
            type: String,
            requied: true,
        },
        checked: {
            type: Boolean,
            requied: false,
            default: true,
        },
        options : {
            required: true,
            type: [Object, Array]
        },
        isEmpty : {
            type: Boolean,
            requied: false,
            default: true,
        },
        isFalse : {
            type: Boolean,
            requied: false,
            default: true,
        }
    },
    methods: {
            /**
             * Xử lý ẩn hiển list-combobox
             * createdBy : PVM.Quân(31/08/2021)
             */
            selectItem(){
                // Hiện 
                var _this = this
                this.isHide = true
                this.isHideIconUp = true
                this.isHideIconDown = false
                this.isFocus = true
                this.dataFilter = this.options
                /**
                 * Khi click ra ngoài windown
                 */
                $(window).on("click.Bst", function(event){
                    if($(`#${'combobox'+_this.name}`).has(event.target).length == 0 && !$(`#${'combobox'+_this.name}`).is(event.target)){
                        // Kiểm tra nếu không phải là list thì ẩn
                        _this.isHide = false
                        _this.isHideIconUp = false
                        _this.isHideIconDown = true
                        _this.isFocus = false
                    }else{
                        // Kiểm tra nếu là list thì không ẩn
                        _this.isHide = true
                        _this.isHideIconUp = true
                        _this.isHideIconDown = false
                        _this.isFocus = true
                        _this.dataFilter = _this.options
                    }
                })
            },

            /**
             * Xử lý khi click vào từng phần tử trong list
             * createdBy : PVM.Quân(31/08/2021)
             */
            showItemListCBB(nameShow,index){
                // Gán value cho input tương ứng với thứ tự của từng phần tử
                this.nameShow = nameShow.toUpperCase()
                this.itemIndex = index
                this.$emit("checkValueCombobox",true,this.nameShow,this.name)
                setTimeout(()=>{
                    this.onBlur()
                },0.001)
                // Ẩn list-combobox
                setTimeout(()=>{
                  this.isHide = false
                  this.isHideIconUp = false
                  this.isHideIconDown = true
                  this.isFocus = false
                },10)
            },
            /**
             * Bắt sự kiện khi nhập vào ô input của combobox
             * createdBy : PVM.Quân(31/08/2021)
             */
            onInput(event){
                 var _this = this
                 
                // Gửi dữ liệu của input lên cha để submit
                this.$emit("checkValueCombobox",true,event.target.value,this.name)
                if(event.target.value == ''){
                    this.dataFilter = this.options
                }
                // Kiểm tra và bắt lỗi validate
                if(event.target.value != ''){
                    this.dataFilter = []
                    this.checkValidate = false
                    this.errorValidate = ''

                    _this.options.forEach((item) => {
                        if (
                            item[_this.name].trim().toLowerCase().includes(event.target.value.toLowerCase()) ||
                            item[_this.code].trim().toLowerCase().includes(event.target.value.toLowerCase())
                        ) {
                            _this.dataFilter.push(item);
                        }
                        });
                }
                if(this.name == 'DepartmentName' && event.target.value == ''){
                    this.checkValidate = true
                    this.errorValidate = 'Đơn vị không được để trống.'
                }
            },

                // Lấy ra id tương ứng với các name 
            onBlur(){
                var _this = this;
                 var id = "";

                this.options.forEach(item=>{
                    if(_this.nameShow.toUpperCase() == item[_this.name].toUpperCase()){
                        id = item[_this.id]
                    }
                })
                // Gửi id và name tương ứng lên cha 
                this.$emit(`sendValueCombobox${this.id}`,id,this.nameShow)
            },
            /**
             * Xử lý sự kiện keydown của list-combobox
             * createdBy : PVM.Quân(31/08/2021)
             */
            keyDown(event){
                var _this = this 
                // Khi down
                if(event.keyCode == 40){
                    this.itemIndex++
                  if(this.itemIndex > this.dataFilter.length){
                    this.itemIndex = 1
                  }
                }
                // khi up
                if(event.keyCode == 38){
                    this.itemIndex--
                  if(this.itemIndex <0){
                    this.itemIndex = this.dataFilter.length
                  }
                }
                // Khi Enter
                if(event.keyCode == 13){
                  // Hiển thị giá trị lấy được
                    this.nameShow = this.dataFilter[_this.itemIndex -1][_this.name].toUpperCase()
                    this.$emit("checkValueCombobox",true,this.nameShow,this.name)
                    this.options.forEach((item,index)=>{
                        if(this.name.toUpperCase() == item[_this.name].toUpperCase()){
                            _this.itemIndex = index +1
                        }
                    })
                  // Ẩn list-combobox
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
                nameShow : '',
                dataFilter : [],
                index : 0,
                isDeleteValue : false,
                itemIndex : 0,
                checkValidate : false,
                errorValidate : '',
            }
    },

    watch: {
        /**
         * Kiểm tra sự thay đổi giá trị của value input
         * createdBy : PVM.Quân(31/08/2021) 
         */
        nameShow : function(){
            // Kiểm tra validate 
            if(this.nameShow != ''){
                    this.checkValidate = false
                    this.errorValidate = ''
                }
        },

        /**
         * Gán dữ liệu lấy được từ Api về cho input
         * createdBy : PVM.Quân(31/08/2021)
         */
        values :function(){
            var _this = this
            this.nameShow = this.values.toUpperCase()
            if(this.values == ''){
                this.itemIndex = 0
            }else{
                this.options.forEach((item,index)=>{
                    if(this.values == item[_this.name]){
                        _this.itemIndex = index +1
                    }
                })
            }
        },

        /**
         * Hiển thị thông báo validate tương ứng với các trường hợp
         * createdBy : PVM.Quân(31/08/2021)
         */
        isEmpty : function(){
            if(this.isEmpty == true){
                // Hiển thị
                this.checkValidate = true
                this.errorValidate = 'Đơn vị không được để trống.'
            }else{
                // Ẩn 
                this.checkValidate = false
                this.errorValidate = ''
            }
        },

        isFalse : function(){
            if(this.isFalse == true){
                // Hiển thị
                this.checkValidate = true
                this.errorValidate = 'Đơn vị không có trong hệ thống.'
            }else{
                // Ẩn 
                this.checkValidate = false
                this.errorValidate = ''
            }
        }

    }
}




// author : PVM.Quân
</script>

<style scoped>
@import url('../../assets/css/base/Combobox.css');
</style>