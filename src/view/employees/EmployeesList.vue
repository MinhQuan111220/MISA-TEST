<template>
    <div class="contents">
        <div class="content">
            <div class="content__body ">
                    <div class="content__action">
                        <div class="content__action-title ">Nhân Viên </div>
                        <div class="content__action-button">
                            <button class="button button-delete" :class="{'active':isDelete}" v-on:click="btnDeleteListOnclick(false)">
                                <i class="far fa-trash-alt"></i>
                                <h3 class="button-text">Xóa Nhân Viên</h3>
                            </button>
                            <button class="button add" v-on:click="btnAddOnclick(false)">
                                Thêm Mới Nhân Viên
                            </button>
                        </div>
                    </div>
                    <div class="content-table">
                        <div class="content__search">
                            <div class="field">
                                <input type="text" autofocus class="field__search " name="search" placeholder="Tìm theo mã, tên nhân viên" v-model="valueSearch">   
                                <div class="icon-search"></div>
                            </div>
                            <div class="icon-loadData" @click="loadData"></div>
                            <div class="icon-exportData" @click="exportData"></div>
                        </div>
                    <Table 
                        :options='employees'
                        id= 'EmployeeId'
                        :arrayTH='arrayThEmployee'
                        propname='propname'
                        name= 'name'
                        checkclass='checkclass'
                        format="format"
                        @trDbOnClick ="trDbOnClick"
                        @sendDeleteList ="getDeleteList"
                        @sendIdEplication="btnEplication" 
                        :value="value"
                        :deleteList="deleteList"
                        :totalPage ="totalPage"
                        :totalRecord="totalRecord"
                        @filter="filter"
                        @sendIdDelete ="btnDeleteById"
                    />
                    </div>
                    
                
            </div>
            <div class="delete-scroll">
            </div>
        
        </div > 
            <EmployeeDetail :isHide='isHideDiaLogDetail' 
        @btnCancelOnclick ="btnCancelOnclick"
        :employeeId='employeeId'
        :mode='mode'
        :arrayEmployeeCode="arrayEmployeeCode"
        :employeeNewCode ="employeeNewCode"
        :departments="departments"
        :isCheckAsk="isCheckAsk"
        :checkSubmitPopUp="checkSubmitPopUp"
        @btnSaveAndAdd="btnSaveAndAdd"
        @toastMessenger="toastMessenger"
        @addEmployee="getEmployeeApi"
        @submitError="errorSubmit"
        @checkDoubleCode="checkDoubleCode"
        ref="employeeModal"
        />
        <PopUp 
        :isHidePopUp="isHidePopUp"
        :isAsk="isAsk"
        :isError="isError"
        :isWarning="isWarning"
        :isDouble="isDouble"
        :code="employeeCode"
        :id="deleteEmployeeId"
        :deleteList="deleteList"
        @isShowForm ="isShowForm"
        @isHideForm="isHideFormPopUp"
        @refeshData="refeshData"
        @btnSubmit="btnSubmit"
        />

        <Loader 
            :isHideLoader="isHideLoader"
        />

        <ToastMessenger
        :isErrorToastMessage="isErrorToastMessage"
        :isShowToast="isShowToast"
        :isSuccessToastMessage="isSuccessToastMessage"
        :isWarningToastMessage="isWarningToastMessage"
        :lable="lable"
        />
    </div>
</template>

<script>

import EmployeeDetail from './EmployeeDetail.vue'
import PopUp from '../../components/base/BasePopUp.vue'
import Table from '../../components/base/BaseTable.vue'
import Loader from '../../components/base/BaseLoader.vue'
import ToastMessenger from '../../components/base/BaseToastMessenger.vue'
import EmployeesAPI from "../../api/components/EmployeesApi";
import DepartmentAPI from "../../api/components/DepartmentsApi.js";
// import MessengerEmployee from "../../constants/MessengerEmployee"
import MessengerError from "../../constants/MessengerError"
import EmployeesApi from '../../api/components/EmployeesApi'


export default {
    name : "EmployeeList",
    components : {
        EmployeeDetail,
        Table,
        PopUp,
        Loader,
        ToastMessenger
    },
    /**
     * Hàm gọi Api lấy dữ liệu
     * creatAt : PVM.Quân (29/07/2021)
     * 
     */
    created() {
        var _this = this

        this.isHideLoader = false
        /**
         * Lấy dữ liệu phòng ban
         * createdBy : PVM.Quân(31/08/2021)
         */
        DepartmentAPI.getAll().then(res =>{
            _this.departments = res.data
            _this.departmentsDropDown = res.data
        }).catch(error =>{
                        _this.consoleError(error.response.status)
                    })

         /**
         * Lấy dữ liệu nhân viên theo phân trang
         * createdBy : PVM.Quân(31/08/2021)
         */
        EmployeesApi.paging(this.employeeFilter,this.pageIndex,this.pageSize).then(res =>{
            _this.totalPage = res.data.totalPage
            _this.totalRecord = res.data.totalRecord
            _this.employees = res.data.data
            _this.isHideLoader = true
            }).catch(error =>{
            _this.consoleError(error.response.status)
            })
    },
    methods: {
        /**
         * Export data
         * createdBy : PVM.Quân(31/08/2021)
         */
        exportData(){
            var _this = this
            EmployeesAPI.exportData(
                _this.employeeFilter,
                _this.pageIndex,
                _this.pageSize,
            ).then((response) => {
                if (response) {
                const blob = new Blob([response.data], {
                    type:
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                });
                const link = document.createElement("a");
                link.href = URL.createObjectURL(blob);
                link.download = "Danh sách nhân viên";
                link.click();
                URL.revokeObjectURL(link.href);
                }
            });
            
        },
        /**
         * Xoá danh sách bị xóa nếu bỏ checkAll
         *  createdBy : PVM.Quân(31/08/2021)
         */
        // removebtnCheckAll(){
        //     this.deleteList = []
        // },
        /**
         * Kiểm tra nếu bị trùng mã
         * createdBy : PVM.Quân(31/08/2021)
         */
        checkDoubleCode(code){
            this.isHidePopUp = false
            this.isAsk = false
            this.isWarning = false
            this.isError = false
            this.isDouble = true
            this.employeeCode = code
        },
        /**
         * Khi thêm và sửa dữ liệu bằng PopUp
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnSubmit(){
            this.checkSubmitPopUp = true
            this.employeeCode = ''
        },
        /**
         * Khi cất và thêm mới
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnSaveAndAdd(){
            this.mode = 0
            this.isHideDiaLogDetail = true
            setTimeout(()=>{
                this.btnAddOnclick()
            },0.001)
        },
        /**
         * Hiển thị popUp tương ứng với các lỗi Validate
         * createdBy : PVM.Quân(31/08/2021)
         */
        errorSubmit(index){
            this.isHidePopUp = false
            this.isAsk = false
            this.isWarning = false
            this.isError = true
            this.isDouble = false
            if(index == 0){
                this.employeeCode = 'Mã không được để trống !'
            }else if(index == 1){
                this.employeeCode = 'Tên không được để trống !'
            }else if(index == 2){
                this.employeeCode = 'Đơn vị không được để trống !'
            }else if(index == 3){
                this.employeeCode = '< Đơn vị > không có trong hệ thống !'
            }
        },

        /**
         * Khi ẩn PopUp thì refrest lại các giá trị
         * createdBy : PVM.Quân(31/08/2021)
         */
        isHideFormPopUp(){
            this.isHidePopUp = true
            this.isHideDiaLogDetail = true
            this.employeeId = ''
            this.employeeNewCode = ''
            this.mode = 0
        },
        /**
         * Khi ẩn PopUp và hiện Form thì refest lại các giá trị
         * createdBy : PVM.Quân(31/08/2021)
         */
        isShowForm(){
            this.isCheckAsk = true
            this.isAsk = false
            this.isWarning = false
            this.checkSubmitPopUp = false
            this.isDouble = false
            this.lable = ''
            this.employeeCode = ''
            this.isHidePopUp = true
            this.$refs.employeeModal.focusInput();
        },

        /**
         * xóa nhân viên theo id
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnDeleteById(id){
            var _this = this 
            EmployeesAPI.get(id).then(res=>{
                _this.employeeCode = res.data.EmployeeCode
                _this.deleteEmployeeId = id
                _this.isHidePopUp = false
                _this.isAsk = false
                _this.isWarning = true
                _this.isError = false
                _this.isDouble = false
            }).catch(error =>{
                 _this.consoleError(error.response.status)
            })
           
        },

        /**
         * Hiển thị form Nhân bản
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnEplication(id){
            this.employeeId = id
            this.mode = 3
            this.isHideDiaLogDetail = false
            this.$refs.employeeModal.focusInput()
            // Gọi lấy dữ liệu Code mới
            this.getNewEmployeeCode()
        },

        /**
         * Ẩn xóa toàn bộ khi xóa thành công
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnConfirmDelete(isDelete){
            this.isDelete = isDelete
            this.isHideLoader = false
        },

         /**
         * Lấy các danh sách cần xóa từ baseTable
         * createdBy : PVM.Quân(31/08/2021)
         */
        getDeleteList(deleteList){
            this.deleteList = deleteList
        },

        /**
         * Load lại data 
         * createdBy : PVM.Quân(31/08/2021)
         */
        loadDataEmployee(){
            var _this = this
            this.isHideLoader = false
            this.isDelete = true
            this.deleteList = []

        /**
         * Lấy dữ liệu nhân viên theo phân trang
         * creatAt : PVM.Quân (29/07/2021)
         */
        EmployeesApi.paging(this.employeeFilter,this.pageIndex,this.pageSize).then(res =>{
            _this.totalPage = res.data.totalPage
            _this.totalRecord = res.data.totalRecord
            _this.employees = res.data.data
            _this.isHideLoader = true
            }).catch(error =>{
            _this.consoleError(error.response.status)
            })
        },
        /**
         * Thoát khỏi form
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnCancelOnclick(checkValue,check){
            
            if(check == 0){
                this.isHideDiaLogDetail = true
                this.employeeId = ''
                this.employeeNewCode = ''
                this.mode = 0
                console.log(checkValue + "" + check)
            }
            else if(check == 1) {
                 if(checkValue == true){
                    this.isHidePopUp = false
                    this.isAsk = true
                    this.isWarning = false
                    this.isError = false
                    this.isDouble = false
                }else{
                    this.isHideDiaLogDetail = true
                    this.employeeId = ''
                    this.employeeNewCode = ''
                    this.mode = 0
                }
            }
        },
       
        /**
         * Hiển thị form chi tiết khi nhấn button thêm nhân viên
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnAddOnclick(){
            this.isHideDiaLogDetail = false
            this.mode = 2
            this.$refs.employeeModal.focusInput()
            // Gọi lấy dữ liệu Code mới
            this.getNewEmployeeCode()
        },

        /**
         * Hiển thị form chi tiết khi Sửa nhân viên
         * createdBy : PVM.Quân(31/08/2021)
         */
        trDbOnClick(employeeId,index){
            this.isHideDiaLogDetail = false
            this.employeeId = employeeId
            this.mode = 1
            this.indexTrdbClick = index
            this.$refs.employeeModal.focusInput();
        },

        /**
         * Khi nhấn nút xóa  tất cả
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnDeleteListOnclick(){
            this.isHidePopUp = false
            this.isWarning = true
            this.isAsk = false
            this.isError = false
            this.isDouble = false
            this.employeeCode = 'Bạn có chắc chắn muốn xóa những nhân viên này không ?'
        },

        /**
         * Hiển thị toast-messenger tương ứng với các thông báo
         * createdBy : PVM.Quân(31/08/2021)
         */
        toastMessenger(isShowToast,lable){
            if(this.isHideLoader == true){
                setTimeout(()=>{
                this.isShowToast = isShowToast
                this.lable = lable
                this.isWarningToastMessage = false
                this.isSuccessToastMessage = true
                this.isErrorToastMessage = false;
                setTimeout(() => {
                    this.isShowToast = true
                }, 5000);
                },100)
            }
        },

        /**
         * Tạo toat-message hiển thị khi thất bại
         * createdBy : PVM.Quân(31/08/2021)
         */
         consoleError(error) {
            var _this = this;
            _this.isShowToast = false;
            _this.isSuccessToastMessage = false;
            _this.isErrorToastMessage = false;
            _this.isWarningToastMessage = true;
            switch (error) {
                case 400:
                _this.lable = MessengerError.Error400
                break;
                case 401:
                _this.lable = MessengerError.Error401
                break;
                case 403:
                _this.lable = MessengerError.Error403
                break;
                case 404:
                _this.lable = MessengerError.Error404
                break;
                case 500:
                _this.lable = MessengerError.Error500
                break;
                default:
                break;
            }
            setTimeout(() => {
                _this.isShowToast = true;
            }, 5000);
        },

        /**
        / Lấy dữ liệu mới khi thêm thành công
        createdBy : PVM.Quân(31/08/2021)
         */
        getEmployeeApi(){
             var _this = this
              _this.isHideLoader = false
              _this.isHidePopUp = true

        /**
         * Lấy dữ liệu nhân viên theo phân trang
         * createdBy : PVM.Quân(31/08/2021)
         */
        EmployeesApi.paging(this.employeeFilter,this.pageIndex,this.pageSize).then(res =>{
            _this.employees = res.data.data
            _this.mode = 4
            _this.isHideLoader = true
            }).catch(error =>{
            _this.consoleError(error.response.status)
            })
        },
        /**
         * Refresh lại bảng và hiện thông báo sau khi xóa thành công 
         * createdBy : PVM.Quân(31/08/2021)
         */
        refeshData(){
        var _this = this
        _this.isHideLoader = false
        // Lấy dữ liệu theo phân trang
        EmployeesApi.paging(this.employeeFilter,this.pageIndex,this.pageSize).then(res =>{
            _this.totalPage = res.data.totalPage
            _this.totalRecord = res.data.totalRecord
            _this.employees = res.data.data
            _this.isHideLoader = true
            _this.isDelete = true
            _this.deleteList = []
                this.isShowToast = false
                this.lable = 'Xóa nhân viên thành công'
                this.isWarningToastMessage = false
                this.isSuccessToastMessage = true
                this.isErrorToastMessage = false;
                setTimeout(() => {
                    this.isShowToast = true
                }, 5000);
            }).catch(error =>{
            _this.consoleError(error.response.status)
            })
        },
        /**
         * Hiển lỗi nễu xóa không thành công
         * createdBy : PVM.Quân(31/08/2021)
         */
        deleteFail(error){
            this.consoleError(error.response.status)
        },

        /**
         * Lọc khi sang trang khác
         * createdBy : PVM.Quân(31/08/2021)
         */
        filter(pageIndex,pageSize){
            this.pageIndex = pageIndex
            this.pageSize = pageSize
            this.isHideLoader = false
            this.getPaging()
        },
        /**
         * Tạo phương thức lấy dữ liệu phân trang
         * createdBy : PVM.Quân(31/08/2021)
         */
        getPaging(){
            var _this = this
            EmployeesApi.paging(this.employeeFilter,this.pageIndex,this.pageSize).then(res =>{
            _this.totalPage = res.data.totalPage
            _this.totalRecord = res.data.totalRecord
            _this.employees = res.data.data
            _this.isHideLoader = true
            }).catch(error =>{
            _this.consoleError(error.response.status)
            })
        },
            /**
             * Lấy dữ liệu newCode mới vào
             * createdBy : PVM.Quân(31/08/2021)
             */  
        getNewEmployeeCode(){
            var _this = this
            EmployeesAPI.get("NewEmployeeCode")
            .then((res) => {
                _this.employeeNewCode = res.data
            })
            .catch((error) => {
                _this.consoleError(error.response.status);
            });
        },

        /**
         * Load lại dữ liệu
         */
        loadData(){
            this.getEmployeeApi()
        }
        
    },


    watch : {
        /**
         * Xử lý ẩn hiện button xóa 
         * createdBy : PVM.Quân(31/08/2021)
         */
        deleteList: function(){
                if(this.deleteList.length < 2){
                    this.isDelete = true
                } else{
                    this.isDelete = false
                }
            },
        /**
            * Xử lý lọc tìm kiếm theo tên,mã sinh viên 
            * createdBy : PVM.Quân(31/08/2021)
         */
        valueSearch : function(value){
            var _this = this
            clearTimeout(this.timeout);
                this.timeout = setTimeout(() => {
                    _this.employeeFilter = value
                    _this.pageIndex = 1
                    _this.pageSize = 10
                    _this.isHideLoader = false
                    _this.getPaging()
                }, 300);
        }
    },
    data() {
        return{
            employees : {},
            employeeId : '',
            employeeCode : '',
            mode : 0,
            isDelete : true,
            employeeIdDelete : [],
            isHideDiaLogDetail : true,
            isHidePopUp : true,
            departments : {},
            isCheckAsk : false,
            checkSubmitPopUp : false,
            deleteList: [],
            arrayEmployeeCode : [],
            isShowToast : true,
            isErrorToastMessage : false,
            isSuccessToastMessage : false,
            isWarningToastMessage : false,
            lable : '',
            isAsk : false,
            isWarning :false,
            isError :false,
            isDouble : false,
            value : '',
            showFormDetail : false,
            isHideLoader : false,
            index : 0,
            isMargin : true,
            valueSearch : '',
            employeeNewCode : '',
            employeeFilter : '',
            departmentId : '',
            pageIndex : 1,
            pageSize : 10,
            totalPage : 0,
            totalRecord : 0,
            timeout: null,
            deleteEmployeeId : "",
            
            /**
             * Mảng chứa các th trong table
             */
            arrayThEmployee : [
                {
                    propname : 'EmployeeCode',
                    name : 'MÃ NHÂN VIÊN',
                    checkclass : '',
                },
                {
                    propname : 'FullName',
                    name : 'HỌ VÀ TÊN',
                    checkclass : ''
                },
                {
                    propname : 'Gender',
                    name : 'GIỚI TÍNH',
                    format : 'gender'
                },
                {
                    propname : 'DateOfBirth',
                    name : 'NGÀY SINH',
                    checkclass : 'between',
                    format : 'date'
                },
                {
                    propname : 'IdentityNumber',
                    name : 'SỐ CMND',
                    checkclass : 'between',
                    format : ''
                },
                {
                    propname : 'PositionName',
                    name : 'CHỨC DANH',
                    checkclass : '',
                },
                {
                    propname : 'DepartmentName',
                    name : 'TÊN ĐƠN VỊ',
                    checkclass : '',
                },
                {
                    propname : 'BankAccount',
                    name : 'SỐ TÀI KHOẢN',
                    checkclass : '',
                    format : '',
                },
                {
                    propname : 'BankName',
                    name : 'TÊN NGÂN HÀNG',
                    checkclass : '',
                    format : ""
                },
                {
                    propname : 'Branch',
                    name : 'CHI NHÁNH',
                    checkclass : '',
                    format : ""
                },
            ]
        }
    },
}
</script>

<style scoped>
@import url('../../assets/css/layout/Content.css');
</style>