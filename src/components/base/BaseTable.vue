<template>
    <div> 
        <div class="staff-table" @scroll="tableOnScroll">
                    <table >
                        <thead>
                            <tr>
                                <th class="bacground-while first"></th>
                                <th class="check"> <input type="checkbox" @input="btnDeleteAllOnClick" :checked="checked" > </th> 
                                    <th :class='item[checkclass]' 
                                        :prop-name='item[propname]' 
                                        v-for="item in arrayTH" :key="item[id]"><span>{{item[name]}}</span>
                                    </th>
                                <th class="menu"><span>CHỨC NĂNG</span></th>
                                <th class="bacground-while end"></th>
                            </tr>
                        </thead>
                        <tbody class="staffTable" v-if="this.options.length > 0"> 
                                <tr v-for="(value,index) in options" :key="value[id]" 
                                @dblclick="trDbOnClick(value[id],index)" 
                                :id=id 
                                :class="[itemExist(checkedId,value[id]) == true ? 'checked' : '' 
                                ,isdbClick == true && index ==indexFocus ? 'focus' : '']"
                                >
                                    <td class="bacground-while first"></td>
                                    <td  class="check"> <input type="checkbox"  :value=value[id] v-model="checkedId" @change="itemExist(checkedId,value[id])"></td>
                                    <td  v-for="td in arrayTH" :key="value[td[id]]" 
                                        :format='td[format]'
                                        :id='[id]' 
                                        :class='td[checkclass]'>{{formatTH(td[format],value[td[propname]])}}</td> 
                                <td class="menu"  :ref="value[id]">
                                    <div class="btn">
                                        <span class="btn-update" @click="btnUpDateOnClick(value[id],index)" :class="{'active':isUpdate == true && index == indexFocus}">Sửa</span>
                                        <div class="btn-down" @click="click(value[id])" :class="[{'focus':idCheck == value[id]},'btn-down'+id]">
                                            <div class="icon-down"></div>
                                        </div>
                                    </div>
                                    
                                </td>
                                <td class="bacground-while end"></td>
                                
                                </tr>
                        </tbody>
                    </table>
             
        </div>
        <div class="empty-data" :class="{'active':this.options.length == 0}">
            <div class="icon-empty-data"></div>
            <div class="value-empty-data">Không có dữ liệu</div>
        </div>
         <div class="list-btn" ref="list-btn">
            <div class="btn-item">
                <div class="btn-sub btn-eplication" @click="btnEplication">Nhân bản</div>
                <div class="btn-sub btn-delete" @click="btnDelete">Xóa</div>
                <div class="btn-sub ">Ngừng sử dụng</div>
            </div>
        </div>
        <div class="content-footer flex items-center" v-if="this.options.length >0 ">
            <div class="record-display flex">Tổng số:  <span style="font-weight : bold ">  {{totalRecord}}  </span> bản ghi</div>
            <div class="paginate items-center" v-if="this.options.length >0 ">
            <DropDown
                    :options="pages"
                    name="value"
                    id="pageSize"
                    @sendValueDropDown="getValueDropDown"
                    :isLast= true
                  />
            <b-pagination
                v-if="this.options.length >0 "
                v-model="pageIndex"
                :total-rows="totalRecord"
                :per-page="pageSize"
                class="paginate-custom"
                prev-text="Trước"
                next-text="Sau"
                limit="4"
                first-number
                last-number
            ></b-pagination>
            </div>
        </div>
    </div>      
</template>


<script>
import Format from '../../untils/Format.js'
import DropDown from './BaseDropDown.vue'

export default {
    name : 'Table',
    components : {
        DropDown
    },
    props :{
        /**
         * Mảng chứa dữ liệu th Api về
         * PVM.Quân (29/07/2021)
         */
        arrayTH : {
            type : [Array,Object],
            requied: true,
        },

        /**
         * mảng chứa dữ liệu tr Api về
         * PVM.Quân (29/07/2021)
         */
        options : {
            type : [Array,Object],
            requied: true,
        },

        /**
         * Lấy tên id
         * PVM.Quân (29/07/2021)
         */
        id : {
            type: String,
            requied: true,
        },
        /**
         * Lấy các prop-name cho th
         * PVM.Quân (29/07/2021)
         */
        propname : {
            type: String,
            requied: true,
        },
        /**
         * Hiển thị tên cho các th
         * PVM.Quân (29/07/2021)
         */
        name : {
            type: String,
            requied: true,
        },
        /**
         * Check xem có tr,th nào cần căn giữa,phải hay không
         * PVM.Quân (29/07/2021)
         */
        checkclass :{
            type: String,
            requied: true,
        },
         /**
         * tên mã 
         * PVM.Quân (29/07/2021)
         */
        code : {
            type: String,
            requied: true,
        },
        /**
         * Lấy các định dạng dữ liệu cho table
         * PVM.Quân (29/07/2021)
         */
        format :{
            type: String,
            requied: true,
        },
        /**
         * Lấy value để thực hiện tìm kiếm
         */
        dataValue :{
            type : Array,
            requied : true,
        },
        /**
         * Lấy value để thực hiện tìm kiếm
         */
        value :{
            type : String,
            requied : true,
        },
        /**
         * Danh sách các tr bị xóa
         */
        deleteList : {
            type : Array,
            requied : true,
        },
        /**
         * Vị trí tr dbClick
         */
        indexTrdbClick : {
            type : Number,
            requied : true,
        },
        totalRecord : {
            type : Number,
            requied : true,
        },
        totalPage : {
            type : Number,
            requied : true,
        }
    },
    methods: {
        /**
         * Chuyển background tr khi checked
         * createdBy : PVM.Quân(31/08/2021)
         */
        itemExist(array,item){
        var check = false;
           if(array.length > 0){
               for(let i = 0 ;i < array.length ; i++){
                   if(array[i]==item){
                       check = true
                   }
               }
           }
           return check
        },

        /**
         * Xử lý khi double click vào tr
         * createdBy : PVM.Quân(31/08/2021)
         */
        trDbOnClick(id,index){
            this.$emit('trDbOnClick',id,index)
            this.isdbClick = true
            this.indexFocus = index
        },

        /**
         * Xử lý khi click vào nút sửa
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnUpDateOnClick(id,index){
            this.$emit('trDbOnClick',id,index)
            this.isdbClick = true
            this.indexFocus = index
            this.isUpdate = true
            setTimeout(()=>{
                this.isUpdate = false
            },200)
        },

        /**
         * Xử lý định dạng các thành phần của table
         * createdBy : PVM.Quân(31/08/2021)
         */
        formatTH(format,value){
            switch (format) {
                    case 'date':
                        return Format.formatDate(value)
                       
                    case 'gender':
                        return Format.formatGender(value)
                    default:
                        return value
                    }
        },

        /**
         * Khi chọn checked tất cả của 1 trang
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnDeleteAllOnClick(){
            var _this = this
            this.index ++
            if(this.checkedId.length < this.options.length){
                this.checkedId = []
            }
            if(this.index == 1){
                this.checked = true
                this.options.forEach((item)=>{
                    this.checkedId.push(item[_this.id])
                })
            }else{
                this.checked = false
                this.index = 0
                _this.options.forEach(()=>{
                    _this.checkedId.pop()
                })
            }
        },

        /**
         * Lấy pageSize tương ứng với giá trị của dropdown
         * createdBy : PVM.Quân(31/08/2021)
         */
        getValueDropDown(pageSize){
            this.pageSize = pageSize
        },

        /**
         * Khi hiển thị danh sách các chức năng 
         * createdBy : PVM.Quân(31/08/2021)
         */
        click(id){
            // Nếu đã hiện click 1 lần nữa để ẩn
            if(this.idCheck === id){
                this.$refs['list-btn'].style.display = 'none'
                this.idCheck = null
            }
            else{
                // Hiện danh sách các chức năng
                if(this.$refs[`${id}`][0].getBoundingClientRect().top == 388.3999938964844 || this.$refs[`${id}`][0].getBoundingClientRect().top ==652.4000244140625){
                    this.$refs['list-btn'].style.top = (this.$refs[`${id}`][0].getBoundingClientRect().top -200 ) + 'px'
                }
                else{
                    this.$refs['list-btn'].style.top = (this.$refs[`${id}`][0].getBoundingClientRect().top -85 ) + 'px'
                }
                this.$refs['list-btn'].style.left = (this.$refs[`${id}`][0].getBoundingClientRect().left - 210) + 'px';
                this.$refs['list-btn'].style.display = 'block'
                this.idCheck = id
            }
        },

        /**
         * Click vào chức năng Nhân bản
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnEplication(){
            this.$emit('sendIdEplication',this.idCheck)
            this.$refs['list-btn'].style.display = 'none'
            this.idCheck = null
        },
        /**
         * Click vào chức năng Xóa
         * createdBy : PVM.Quân(31/08/2021)
         */
        btnDelete(){
            this.$emit('sendIdDelete',this.idCheck)
            this.$refs['list-btn'].style.display = 'none'
            this.idCheck = null
        },

        /*
        * Khi onscroll table
        *createdBy : PVM.Quân(03/09/2021)
        */
        tableOnScroll(){
            this.idCheck = null
            this.$refs['list-btn'].style.display = 'none'
        }
    },

    data() {
        return {
            arrayTdHTML : [],
            checkedId: [],
            checkNum : 0,
            index : 0,
            idCheck : null,
            isHide : false,
            indexChecked : -1,
            isdbClick : false,
            indexFocus : -1,
            pageSize : 10,
            pageIndex : 1,
            sumPage : 0,
            currentPage: 1,
            isUpdate : false,
            checked : false,
            arrayDeleteList : [],

            // tạo các pageSize mặc định
            pages : [
                { value: "10 bản ghi trên 1 trang", pageSize: 10 },
                { value: "20 bản ghi trên 1 trang", pageSize: 20 },
                { value: "30 bản ghi trên 1 trang", pageSize: 30 },
                { value: "50 bản ghi trên 1 trang", pageSize: 50 },
                { value: `100 bản ghi trên 1 trang `, pageSize: 100},
            ]
        }
    },
    watch: {
        
        /**
         * Gửi các tr được check để xóa lên cha 
         * createdBy : PVM.Quân(31/08/2021)
         */
        checkedId: function() {
            this.$emit('sendDeleteList', this.checkedId)
            if(this.checkedId.length < this.options.length){
                this.index = 0
                this.checked = false
            }
            console.log(this.checkedId)
        },

        /**
         * Gán lại id xóa khi bị thay đổi
         * createdBy : PVM.Quân(31/08/2021)
         */
        deleteList : function(){
            this.checkedId = this.deleteList
        },

        /**
         * Load lại dữ liệu khi pageSize thay đổi
         * createdBy : PVM.Quân(31/08/2021) 
        */        
        pageSize : function(){
            this.$emit('filter',this.pageIndex,this.pageSize)
        },

        /**
         * Load lại dữ liệu khi pageIndex thay đổi
         * createdBy : PVM.Quân(31/08/2021)
         */
        pageIndex : function(){
            this.$emit('filter',this.pageIndex,this.pageSize)
        },

        /**
         * Kiểm tra sự thay đổi của danh sách xóa nhiều
         * createdBy : PVM.Quân(31/08/2021)
         */
        options : function (){
        var _this = this
                this.index = 0
                   this.checked = false
            this.options.forEach(item=>{
                this.checkedId.forEach(id=>{
                    if(item[_this.id] == id){
                        this.checked = true
                        this.index = 1
                    }
                })
            })
        },

        /**
         * Xử lý sự kiện khi thay đổi tông danh sách
         * createdBy : PVM.Quân(31/08/2021)
         */
        totalRecord : function(){
            if(this.totalRecord < this.pageSize || this.totalRecord == 0){
                this.pageIndex = 1
            } 
           
        },
        /**
         * Xử lý sự kiện khi tổng số page bị thay đổi
         * createdBy : PVM.Quân(31/08/2021)
         */
        totalPage : function(){
            this.sumPage = this.totalPage>0 ? this.totalPage : 1
             if(this.pageIndex > this.totalPage){
                this.pageIndex = this.totalPage
            }
            if(this.totalPage == 0){
                this.pageIndex = 1
            }
        },
    },
    
}
</script>

<style scoped>
@import url('../../assets/css/base/Table.css');
</style>