<template>
    <div id="pop-up-delete" class="pop-up" :class="{'show':isShowPopUp}">
            <div class="pop-up-content" >
                <div class="pop-up-info">
                    <div class="icon-popup" :class="{'icon-ask':isAsk,'icon-error':isError,'icon-warning':isWarning,'icon-double':isDouble}">
                        
                    </div>
                    <div class="pop-up-description" :class="{'isDouble':isDouble}">
                        <span>{{value}}</span>
                    </div>
                </div>
                <div class="pop-up-line"></div>
                <div class="div__popup-btn" :class="{'active':isAsk}">
                    <div class="button " @click="btnCancel">
                        Hủy
                    </div>
                    <div class="div-btn-popup">
                        <div class="button" @click="btnExit">
                            Không
                        </div>
                        <div class="button add" @click="btnSave">
                            Có 
                        </div>
                    </div>
                </div>
                <div class="popup-btn-close" :class="{'active':isError}">
                    <div class="button add" @click="btnCancel">Đóng</div>
                </div>
                <div class="popup-btn-delete" :class="{'active':isWarning}">
                    <div class="button" @click="btnExit">Không</div>
                    <div class="button add" @click="btnSave">
                            Có 
                    </div>
                </div>
                <div class="popup-btn-double" :class="{'active':isDouble}">
                    <div class="button add" @click="btnCancel">
                           Đồng ý
                    </div>
                </div>
            </div>
    </div>
   
</template>


<script>
import EmployeesAPI from "../../api/components/EmployeesApi";

export default {
    name : "PopUp",
    props :{
        isHidePopUp : {
            type : Boolean,
            default : true,
            required : true,
        },
        isAsk : {
            type : Boolean,
            default : false,
            required : true,
        },
        isWarning : {
            type : Boolean,
            default : false,
            required : true,
        },
        isError : {
            type : Boolean,
            default : false,
            required : true,
        },
        isDouble : {
            type : Boolean,
            default : false,
            required : true,
        },
        deleteList :{
            type : Array,
            required : true,
        },
        code : {
            type : String,
            default : '',
            required : true
        },
        id : {
            type : String,
            default : '',
            required : true
        }
    },
    methods: {
        /**
         * Xử lý sự kiện ẩn PopUp nhưng không thoát khỏi form
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnCancel (){
            // Gửi lên cha để thực hiện việc không ẩn form
            this.$emit('isShowForm',true)

            // Ẩn PopUp
            this.isShowPopUp = true
        },

        /**
         * Xử lý sự kiển ẩn PopUp và ẩn form
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnExit(){
            // Gửi dữ yêu cầu lên cha để ẩn form
            this.$emit('isHideForm')

            // Ẩn form 
            this.isShowPopUp = true
        },

        /**
         * Thực hiện các chức năng liên quan đến APi
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnSave(){
            var _this = this
            // Khi xóa
            if(this.isWarning == true){
                if(this.deleteList.length > 1){
                    EmployeesAPI.deleteListId(JSON.stringify(this.deleteList))
                    .then(()=>{
                        _this.$emit("refeshData")
                        _this.$emit('isHideForm')
                        _this.isShowPopUp = true
                    }).catch(error =>{
                        _this.$emit("deleteFail",error)
                    })
                }else{
                    EmployeesAPI.delete(_this.id).then(()=>{
                        _this.$emit("refeshData")
                        _this.$emit('isHideForm')
                        _this.isShowPopUp = true
                    }).catch(error =>{
                        _this.$emit("deleteFail",error)
                    })
                }
            }
            // Khi thêm và sửa
            if(this.isAsk == true){
                this.$emit('btnSubmit')
            }
        },
        /**
         * Xử lý khi xóa vĩnh viễn thông tin nhân viên
         * PVM.Quân (29/07/2021)
         */
        btnDeleteOnClick(){
            var _this = this
            _this.$emit('btnConfirmDelete',true)
            if(this.deleteList.length==1){
                 EmployeesAPI.delete(this.deleteList[0]).then(() => {
                         _this.$emit("refeshData")
                       
                    }).catch(error =>{
                        _this.$emit("deleteFail",error)
                    })
            }else{
                
               var lisId = []
               this.deleteList.forEach((id,index)=>{
                   lisId[index]=id
               })
                 EmployeesAPI.deleteListId(JSON.stringify(lisId)).then(() => {
                         _this.$emit("refeshData")
                       
                    }).catch(error =>{
                        _this.$emit("deleteFail",error)
                    })
                // console.log(lisId)
            }
            this.$emit('btnDeleteOnclick',true) 
        }, 
    },

    data() {
        return {
            employees : {},
            value : '',
            isShowPopUp : true,
        }
    },

    watch : {
        /**
         * Xử lý ẩn hiện PopUp
         * createdBy : PVM.Quân(31/08/2021)
         */
        isHidePopUp : function(){
            this.isShowPopUp = this.isHidePopUp
        },

        /**
         * Xử lý hiện PopUp khi thoát mà dữ liệu bị thay đổi
         * createdBy : PVM.Quân(31/08/2021)
         */
        isAsk : function(){
            this.value = "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
        },

        /**
         * Hiện thị thông báo tương ứng với mỗi PopUp
         * createdBy : PVM.Quân(31/08/2021)
         */
        code : function(){
            var _this = this 
            // Khi xóa
            if(this.isWarning == true){
                if(this.deleteList.length >1){
                    this.value = this.code
                }else{
                    this.value = `Bạn có thực sự muốn xóa Nhân Viên < ${_this.code} > không ?`
                }
                 
            }
            // Khi bị lỗi validate
            if(this.isError == true){
                this.value = this.code
            }
            // Khi bị trùng mã code
            if(this.isDouble == true){
                this.value = `Mã nhân viên < ${this.code} > đã tồn tại trong hệ thống vui lòng kiểm tra lại `
            }
        }
    }
    
}

</script>

<style scoped>
@import url('../../assets/css/form/FormWarning.css');
</style>