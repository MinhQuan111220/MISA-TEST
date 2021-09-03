<template>
  <div :class="'base-input' + id" class="input-embrace">
    <div class="form-group" :class="{'invalid':checkValidate}">
      <h3 class="input-header label-field__input">
        <div v-if="binding == true">
          {{ value }}<span class="correct">*</span>
        </div>
        <div v-else>
          {{ value }}
        </div>
      </h3>
        <DxDateBox
          v-if="type === 'date'"
          :value="nameInput"
          :onValueChanged="onChange"
          displayFormat="dd/MM/yyyy"
          placeholder="DD/MM/YYYY"
          :useMaskBehavior="true"
          :showClearButton="false"
          class="dxDateBox"
          dateOutOfRangeMessage="Ngày chọn không được quá ngày hiện tại"
          :max="new Date()"
          
        />
        <input
          v-else
          :type="type"
          class="input"
          :id="id"
          v-model="nameInput"
          :format="type"
          :autofocus="autofocus"
          @blur="onBlur"
          @input="onInput"
          :title="errorValidate"
        />
    </div>
  </div>
</template>

<script>
// import Format from "../../untils/Format.js";
import DxDateBox from "devextreme-vue/date-box";
import { locale } from "devextreme/localization";
locale("vi-VN");
export default {
  name: "Input",
  components: {
    DxDateBox,
  },
  props: {
    autofocus :{
      type : Boolean,
      required : false,
    },
    name: {
      type: [String, Number, Date],
      required: true,
    },
    id: {
      type: String,
      required: true,
    },
    value: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      required: true,
    },
    binding: {
      type: Boolean,
      required: false,
    },
    isFocus: {
      type: Boolean,
      default: false,
    },
    isCode :{
      type: Boolean,
      default: false,
    },
    isFullName :{
      type: Boolean,
      default: false,
    }
  },
  methods: {
    /**
     * Truyền dữ liệu lấy được từ input lên cha khi Blur 
     * createdBy : PVM.Quân(31/08/2021)
     */
    onBlur(){
        var value = this.nameInput
        this.$emit("sendValueInput", value, this.id);
    },

    onChange(event){
      if(this.type == 'date'){
          this.nameInput = event.value
         this.$emit("sendValueInput", event.value, this.id);
         this.$emit("checkValue",true,event.value,this.id)
      }
    },
    /**
     * Truyền dữ liệu lấy được từ input khi onInput và validate dữ liệu
     * createdBy : PVM.Quân(31/08/2021)
     */
    onInput(event){
      // Truyền dữ liệu 
      this.$emit("checkValue",true,event.target.value,this.id)

      // Kiểm tra dũ liệu 
      if(event.target.value != ''){
        // Ẩn validate
        this.checkValidate = false
        this.errorValidate = ''
      }
      // Hiển thị các validate tương ứng
      if(this.id == 'EmployeeCode' && event.target.value == ''){
        this.checkValidate = true
        this.errorValidate = 'Mã không được để trống.'
      }else if(this.id == 'FullName' && event.target.value == ''){
        this.checkValidate = true
        this.errorValidate = 'Tên không được để trống.'
      }
      
    }
  },
  watch: {
    /**
     * Hiển thị dữ liệu của ô input khi lấy dữ liệu về
     * createdBy : PVM.Quân(31/08/2021)
     */
    name: function () {
      this.nameInput = this.name;
    },

    /**
     * Kiểm tra nếu lỗi về Code để trống
     * createdBy : PVM.Quân(31/08/2021)
     */
    isCode : function(){
      if(this.isCode == true){
        // Hiện validate
        this.checkValidate = true
        this.errorValidate = 'Mã không được để trống !'
      }else{
        // Ẩn validate
         this.checkValidate = false
          this.errorValidate = ''
      }
    },

    /**
     * Kiểm tra nếu tên để trống
     * createdBy : PVM.Quân(31/08/2021)
     */
    isFullName : function(){
      if(this.isFullName == true){
        // Hiện validate
        this.checkValidate = true
        this.errorValidate = 'Tên không được để trống !'
      }else{
        // Ẩn validate
        this.checkValidate = false
        this.errorValidate = ''
      }
    }
  },
  data() {
    return {
      nameInput: null,
      typeInput: "",
      checkValue : false,
      checkValidate : false,
      errorValidate : '',
    };
  },
};
</script>

<style >
@import url("../../assets/css/form/Staff_information.css");
@import url("../../assets/css/base/Input.css");
@import url("../../assets/css/base/Vadilator.css");
</style>