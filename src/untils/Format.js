/**
 * File xử lý các kiểu định dạng
 * creatBy : PVM.Quân(29/07/2021)
 */



class Format{
    
    constructor(){
       
    }


    
    
    /**
     * Xử lý hiển thị ngày tháng
     * creatBy : PVM.Quân(29/07/2021)
     */
     formatDate(dateformat){
        var date = new Date(dateformat);
            var day = date.getDate(),
            month = date.getMonth() +1,
            year = date.getFullYear();
            
            day = day<10 ? '0'+day : day
            month = month<10 ? '0'+month : month
            return day + '/'+month+'/'+year
    }
     /**
     * Format vao modal
     * creatBy : PVM.Quân(29/07/2021)
     */
    formatDateToModal(dateformat){
        var date = new Date(dateformat);
            var day = date.getDate(),
            month = date.getMonth() +1,
            year = date.getFullYear();
            
            day = day<10 ? '0'+day : day
            month = month<10 ? '0'+month : month
            return year + '-'+month+'-'+day
    }
    /**
     * Xử lý khi string rỗng thì ẩn di
     * creatBy : PVM.Quân(29/07/2021)
     */
     formatNull(value){
        if(value == 'null'.toLowerCase() || value == undefined){
            return ''
        }
        else return value
    }
    
    /**
     * Xử lý khi các phần tử trong mảng bị trùng
     * creatBy : PVM.Quân(29/07/2021)
     */
    
     listArray(arr){
        let isExist = (arr, x) => arr.indexOf(x) > -1;
        let ans = [];
    
        arr.forEach(element => {
            if(element !==''){
                if(!isExist(ans, element)) ans.push(element);
            }
        });
    
        return ans;
    }
    /**
     * Hàm định dạng giới tính 
     * creatBy : PVM.Quân(29/07/2021)
     */
    formatGender(value) {
        if(value == 0){
            return 'Nam' 
        }else if(value == 1){
            return 'Nữ'
        }else{
            return 'Khác'
        }
    }

    /**
     * Hàm kiểm tra tồn tại trong mảng
     * creatBy : PVM.Quân(31/08/2021)
     */
    itemExist(array,item){
        if(array.include(item)==true){
            return true;
        }
        return false;
    }
    /**
     * Hàm kiểm tra tồn tại trong mảng khi update
     * creatBy : PVM.Quân(31/08/2021)
     */
    checkDoubleUpdate(array,value,valueCheck)
        {
            for( var i = 0; i < array.length; i++){ 
                if ( array[i] === valueCheck) {
                  array.splice(i, 1); 
                }
             }
            if(this.checkDoubleAdd(array,value)==true){
                return true;
            }
            return false;
        }
    
    /**
     * Kiểm tra phần tử tồn tại trong mảng
     * creatBy : PVM.Quân(31/08/2021)
     */
    checkDoubleAdd(array,value){
        for(var i=0;i<array.length;i++){
            if(array[i] == value){
                return true;
            }
        }
        return false
    }
}


export default new Format()