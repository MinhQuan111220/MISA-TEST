<template>
  <div class="form" :class="{ active: isHide }">
    <div class="container">
      <div class="form__person">
        <div class="form-staff">
          <div class="form__header">
            <div class="form__header-heading">
              <span class="heading"
                >THÔNG TIN NHÂN VIÊN</span
              >
            <div class="check-person">
              <input type="checkbox" class="check-person__input"> 
              <span>Là khách hàng</span>
            </div>
            <div class="check-person">
              <input type="checkbox" class="check-person__input"> 
              <span>Là nhà cung cấp</span>
            </div>
            </div>
            <div class="icon-form">
                <div class="icon icon-what"></div>
                <div class="icon icon-exit" @click="btnExit"></div>
            </div>
          </div>
           <ul class="form__general row">
                <li class="form__general-item left col-lg-6 ">
                  <div class="form-input col-lg-5 left">
                    <Input
                    :name="mode == 1 ? (employee.EmployeeCode == null ? '':employee.EmployeeCode) : (employeeNewCode == null ? '': employeeNewCode)" 
                    :binding="true"
                    :isFocus="true"
                    value="Mã "
                    id="EmployeeCode"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue"
                    :isCode="isCode"
                    :autofocus ="autofocus"
                    ref="employeecode-input"
                  />
                  </div>
                  <div class="form-input col-lg-7">
                    <Input
                    :name="employee.FullName == null ? '' : employee.FullName" 
                    :binding="true"
                    :isFocus="true"
                    value="Tên "
                    id="FullName"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue"
                    :isFullName="isFullName"
                    @btnCancel="btnCancel"
                    :autofocus ="autofocus"
                    ref="fullName"
                  />
                  </div>
                </li>
                <li class="form__general-item col-lg-6">
                  <div class="form-input col-lg-5 left">
                    <Input
                    :name="employee.DateOfBirth ==null ? '' : employee.DateOfBirth" 
                    :isFocus="true"
                    value="Ngày sinh "
                    id="DateOfBirth"
                    type="date"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue"
                    @btnCancel="btnCancel"
                    :autofocus ="autofocus"
                    lable="" 
                  />
                  </div>
                  <div class="form-input rigth col-lg-7">
                    <h3 class="input-header label-field__input">
                        Giới tính
                    </h3>
                    <div class="form-input__checkbox">
                      <div class="checkbox-item" :class="{'checked':gender ==0}" @click="btnGender(0)">
                        <input type="checkbox" class=""> 
                      </div>
                        <span>Nam</span>
                      <div class="checkbox-item" :class="{'checked':gender ==1}" @click="btnGender(1)">
                        <input type="checkbox" class=""> 
                      </div>
                        <span>Nữ</span>
                      <div class="checkbox-item" :class="{'checked':gender ==2}" @click="btnGender(2)">
                        <input type="checkbox" class=""> 
                      </div>
                        <span>Khác</span>
                    </div>
                  </div>
                </li>
                <li class="form__general-item left form-combobox col-lg-6">
                  <h3 class="input-header label-field__input">Đơn vị <span class="check">*</span></h3>
                  <ComBoBox 
                    :options='departments'
                    :values="employee.DepartmentName"
                    :isEmpty="isEmpty"
                    :isFalse="isFalse"
                    name= "DepartmentName"
                    code="DepartmentCode"
                    id= "DepartmentId"
                     @sendValueComboboxDepartmentId="getValueComboboxDepartmentId"
                     @checkValueCombobox="checkValueCombobox"
                  />
                </li>
                <li class="form__general-item col-lg-6">
                  <div class="form-input col-lg-7 left">
                    <Input
                    :name="employee.IdentityNumber == null ? '' : employee.IdentityNumber" 
                    :isFocus="true"
                    value="Số CMND "
                    id="IdentityNumber"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue"
                    @btnCancel="btnCancel" 
                  />
                  </div>
                  <div class="form-input col-lg-5">
                    <Input
                    :name="employee.IdentityDate == null ? '' : employee.IdentityDate" 
                    :isFocus="true"
                    value="Ngày cấp "
                    id="IdentityDate"
                    type="date"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue"
                    @btnCancel="btnCancel" 
                  />
                  </div>
                </li>
                <li class="form__general-item left col-lg-6">
                  <Input
                    :name=" employee.PositionName == null ? '' : employee.PositionName"
                    value="Chức danh"
                    id="PositionName"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                </li>
                <li class="form__general-item col-lg-6">
                  <Input
                    :name="
                      employee.IdentityPlace == null
                        ? ''
                        : employee.IdentityPlace
                    "
                    value="Nơi cấp"
                    id="IdentityPlace"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                </li>
                <li class="form__general-item top col-lg-12">
                  <Input
                    :name="employee.Address == null ? '' : employee.Address"
                    value="Địa chỉ"
                    id="Address"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                </li>
                <li class="form__general-item col-lg-12">
                  <div class="form-input col-lg-3 left">
                    <Input
                    :name="employee.MobiPhoneNumber == null ? '' : employee.MobiPhoneNumber" 
                    value="ĐT di động "
                    id="MobiPhoneNumber"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                  </div>
                  <div class="form-input col-lg-3 left">
                    <Input
                    :name=" employee.TelePhoneNumber == null ? '' : employee.TelePhoneNumber" 
                    value="ĐT cố định "
                    id="TelePhoneNumber"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                  </div>
                  <div class="form-input col-lg-3 left">
                    <Input
                    :name="employee.Email == null ? '' : employee.Email" 
                    value="Email "
                    id="Email"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                  </div>
                </li>
                <li class="form__general-item col-lg-12">
                  <div class="form-input col-lg-3 left">
                    <Input
                    :name="employee.BankAccount == null ? '' : employee.BankAccount" 
                    value="Tài khoản ngân hàng "
                    id="BankAccount"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                  </div>
                  <div class="form-input col-lg-3 left">
                    <Input
                    :name="employee.BankName == null ? '' : employee.BankName" 
                    value="Tên ngân hàng "
                    id="BankName"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                  </div>
                  <div class="form-input col-lg-3 left">
                    <Input
                    :name="employee.Branch == null ? '' : employee.Branch" 
                    value="Chi nhánh "
                    id="Branch"
                    type="text"
                    @sendValueInput="getValueInput"
                    @checkValue="checkValue" 
                  />
                  </div>
                </li>
              </ul>
          <div class="div-btn">
            <div class="button" @click="btnCancel">Hủy</div>
            <div class="btn-submit">
              <button
                  class="button"
                  id="button-save"
                  @click="btnSave(0)"
              >
                Cất
              </button>
              <button
                  class="button button-save"
                  id="button-save-add"
                  @click="btnSave(1)"
              >
                Cất và Thêm
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// Author : PVM.Quân (29/07/2021)
import ComBoBox from "../../components/base/BaseComBoBox.vue"
import Input from "../../components/base/BaseInput.vue";
import EmployeesAPI from "../../api/components/EmployeesApi";
import Format from "../../untils/Format.js";
import MessengerEmployee from "../../constants/MessengerEmployee"

export default {
  name: "EmployeeDetail",
  components: {
    ComBoBox,
    Input,
  },
  props: {
    isHide: {
      type: Boolean,
      default: true,
      required: true,
    },
    employeeId: {
      type: String,
      default: "",
      required: true,
    },
    mode: {
      type: Number,
      default: 0, 
      required: true,
    },
    employeeNewCode: {
      type: String,
      default: "",
      required: true,
    },
    departments : {
      type : [Array,Object],
      required : true,
    },
    isCheckAsk : {
      type : Boolean,
      default : false,
      required : true,
    },
    checkSubmitPopUp :{
      type : Boolean,
      default : false,
      required : true,
    },
  },

  methods: {
    /**
     * Bắt sự kiện focus mặc định vào ô input đầu tiên của Form
     * createdBy : PVM.Quân(31/08/2021)
     */
    focusInput(){
      this.$nextTick(() => {
        this.$refs['employeecode-input'].$el.childNodes[0].childNodes[1].focus();
      })
    },

    /**
     * Kiểm tra sự thay đổi của ô input khi onInput và bắt các sự kiện validate
     * createdBy : PVM.Quân(31/08/2021)
     */
    checkValue(check,value,id){
      this.checkValueOnInput = check
      if(value == '' && id == "EmployeeCode"){
        this.isCode = true
      }else if(value == '' && id == "FullName"){
        this.isFullName = true
      }
    },
    /**
     * Kiểm tra sự thay đổi của ô input của combobox khi onInput và bắt các sự kiện validate
     * createdBy : PVM.Quân(31/08/2021)
     */
    checkValueCombobox(check,value,name){
      this.checkValueOnInput = check
      if(value == '' && name == "DepartmentName"){
        this.isEmpty = true
      }
    },

    /**
     * Gán các giá trị của combobox vào phần tử Employee tương ứng
     * createdBy : PVM.Quân(31/08/2021)
     */
    getValueComboboxDepartmentId(id,name){
      if(this.mode == 1){
        if(id != this.employee.DepartmentId && name != this.employee.DepartmentName){
          this.employee.DepartmentId = id
          this.employee.DepartmentName = name
          this.checkValueForm = true
        }else{
          this.checkValueForm = false
        }
      }else{
         this.employee.DepartmentId = id
          this.employee.DepartmentName = name
      }
      this.isEmpty = false
      this.isFalse = false
    },

    /**
      * Gán giá từ ô input tương ứng vs phần tử Employee
      * createdBy : PVM.Quân(31/08/2021) 
    */  
    getValueInput(value, id) {
        if(this.mode == 1){
          if(this.employee[id] != value){
          this.checkValueForm = true
          }else{
            this.checkValueForm = false
            this.checkValueOnInput = false
          }
            this.employee[id]=value
        }else{
          this.employee[id] = value
          this.checkValueOnInput = false
        }
      },
    
    /**
     * Gán giá trị các radio tương ứng với phần tử Employee
     * createdBy : PVM.Quân(31/08/2021)
     */
    btnGender(value){
      if(this.mode == 1){
        if(this.employee.Gender != value){
        this.employee.Gender = value
        this.checkValueForm = true
        }else{
          this.checkValueForm = false
        }
      }
      this.gender = value
      this.checkValueOnInput = true
    },

    /**
     * Xử lý khi nhấn nút hủy form
     * createdBy : PVM.Quân(31/08/2021)
     */
    btnCancel() {
        this.$emit("btnCancelOnclick", this.checkValueOnInput,0);
    },
    
    /**
     * Xử lý khi nhẫn nút X
     * createdBy : PVM.Quân(31/08/2021)
     */
    btnExit() {
          this.$emit("btnCancelOnclick", this.checkValueOnInput,1);
    },

    /**
     * Khi Cất hoặc Cất và Thêm
     * createdBy : PVM.Quân(31/08/2021)
     */
    btnSave(index){
      var _this = this
      this.checkSubmit = true
      // Kiểm tra đơn vị có trong hệ thống hay không
      if(this.employee.DepartmentName != '' || this.employee.DepartmentName != undefined){
          let index = 0
          this.departments.forEach(item=>{
            if(this.employee.DepartmentName.toUpperCase() != item['DepartmentName'].toUpperCase() && this.employee.DepartmentName != undefined){
                index ++
            }
          })
          if(index == this.departments.length){
            this.$emit('submitError',3)
            this.checkSubmit = false
            this.isFalse = true
            this.isEmpty = false
          }
        }
      
      // Kiểm tra đơn vị trống hay không
      if(this.employee.DepartmentName == '' || this.employee.DepartmentName == undefined){
        this.$emit('submitError',2)
        this.checkSubmit = false
        this.isEmpty = true
        this.isFalse = false
      }
      // Kiểm tra Tên trống hay không
      if(this.employee.FullName == '' || this.employee.FullName == undefined){
        this.$emit('submitError',1)
        this.checkSubmit = false
        this.isFullName = true
      }
      // Kiểm tra mã trống hay không
      if(this.employee.EmployeeCode == '' ){
        this.$emit('submitError',0)
        this.checkSubmit = false
        this.isCode = true
      } 
      if(this.checkSubmit == true){
          if((_this.mode == 1 && _this.employee.EmployeeCode == _this.employeeCode)){
            _this.put(index)
          }else{
            // Gọi Api lấy mảng chứa các EmployeeCode để kiểm tra
            EmployeesAPI.getByProperTy('EmployeeCode').then(res=>{
              _this.arrayEmployeeCode = res.data
              if(_this.mode == 1){
                if(Format.checkDoubleUpdate(_this.arrayEmployeeCode,_this.employee.EmployeeCode,_this.employeeCode) == true){
                  _this.checkSubmit = false
                  _this.$emit('checkDoubleCode',_this.employee.EmployeeCode)
                }
              }else{
                if(Format.checkDoubleAdd(_this.arrayEmployeeCode,_this.employee.EmployeeCode) == true){
                  _this.checkSubmit = false
                  _this.$emit('checkDoubleCode',_this.employee.EmployeeCode)
                }
              }
              // Thực hiện submit
              if(_this.checkSubmit == true){
                if(_this.mode == 1){
                  _this.put(index)
                }else{
                  _this.post(_this.employee,index)

                }
              }
            }).catch(error=>{
              _this.$emit('consoleError',error)
            })
          
          }
      }
      
    },
    /**
     * Tạo phương thức post
     * createdBy : PVM.Quân(31/08/2021)
     */
    post(employee,index) {
      var _this = this;
      if(this.employee.Gender == undefined){
        this.employee.Gender = 0
      }
      EmployeesAPI.post(employee)
        .then(() => {
          _this.$emit("toastMessenger", false, MessengerEmployee.AddCorrect);
          _this.$emit("addEmployee")
          if(index == 0){
            this.btnCancel()
          }else{
            this.$emit('btnSaveAndAdd')
          }
        })
        .catch((error) => {
          _this.$emit('consoleError',error)
        });
    },
    /**
     * Tạo phương thức put và kiểm tra xem có thay đổi gì không
     * createdBy : PVM.Quân(31/08/2021)
     */
    put(index) {
        var _this = this;
        if(this.checkValueForm == true){
          EmployeesAPI.put(_this.employeeId, _this.employee)
            .then(() => {
              _this.$emit("toastMessenger", false, MessengerEmployee.UpdateCorrect);
              _this.$emit("addEmployee")
               if(index == 0){
                  this.btnCancel()
              }else{
                  this.$emit('btnSaveAndAdd')
              }
            })
            .catch((error) => {
              _this.$emit('consoleError',error)
            });
        }else{
           if(index == 0){
                  this.btnCancel()
              }else{
                  this.$emit('btnSaveAndAdd',)
              }
        }
       
    },


    /**
     * Tạo phương thức lấy dữ liệu theo Id
     * createdBy : PVM.Quân(31/08/2021)
     */
    getEmployeeById(){
         var _this = this;
        EmployeesAPI.get(_this.employeeId)
          .then((res) => {
            _this.employee = res.data;
            _this.employeeCode = this.employee.EmployeeCode
            _this.gender = _this.employee.Gender
          })
          .catch((error) => {
            _this.$emit('consoleError',error)
          });
    },
  },

  data() {
    return {
      check: 0,
      gender : 0,
      checkValueOnInput : false,
      checkSubmit : true,
      isCode : false,
      isFullName :false,
      isEmpty : false,
      isFalse : false,
      employee : {
          EmployeeCode: "",
          FullName: "",
          DateOfBirth: null,
          Gender : 0,
          IdentityNumber: "",
          IdentityDate: null,
          IdentityPlace: "",
          Email: "",
          MobiPhoneNumber: "",
          TelePhoneNumber: "",
          PositionName: "",
          DepartmentName: "",
          BankAccount: "",
          BankName: "",
          Branch: "",
      },
      employeeCode: "",
      arrayEmployeeCode : [],
      identityNumber :" ",
      isShowToast: "",
      isSuccess: "",
      isWarning: "",
      isError : "",
      valueInput : '',
      idInput : '',
      autofocus : false,
      checkValueForm : false,
      isDoubleCode : false,
      checkForm : 1,
      isRequired : true,
    };
  },

  watch: {
    /**
     * Phân biệt thi thêm và khi sửa và khi ở trang chính
     * PVM.Quân (29/07/2021)
     */
    mode: function () {
    this.checkValueOnInput = false
      // Khi thêm
      if (this.mode == 2) {
          this.employee = {};
          this.check = 0;
          this.checkForm = 2
          this.employee.EmployeeCode = this.employeeNewCode
          this.employee.DepartmentName = ''
          this.gender =0
      } 
      // Khi ở trang chính
      if(this.mode ==0){
        this.gender = 0
        this.isFullName = false
        this.isCode = false
        this.isEmpty = false
      }
      // Khi sửa
      else {
        this.check = 1;
        this.checkForm = 1
      }
    },

    /**
      * Load dữ liệu để lấy thông tin theo nhân viên
      * createdBy : PVM.Quân(31/08/2021)
    */
    employeeId : function(){
      if(this.employeeId != ''){
        this.getEmployeeById()
      }
    },

    /**
     * Xử lý khi submit bằng PopUp
     * createdBy : PVM.Quân(31/08/2021)
     */
    checkSubmitPopUp(){
      if(this.checkSubmitPopUp == true){
        this.checkSubmit = true
        setTimeout(()=>{
          this.btnSave(0)
        },0.001)
      }
    }
  },
};
</script>

<style scoped>
@import url("../../assets/css/form/Staff_information.css");
</style>