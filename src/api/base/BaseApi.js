import BaseAPIConfig from "../base/BaseApiConfig";

export default class BaseAPI {
  constructor() {
    this.controller = null;
  }// anth muon ti controller la cai gi vay
  /**
   * Phương thức lấy tất cả dữ liệu
   */
  getAll() {
    return BaseAPIConfig.get(`${this.controller}`); 
  }
  /**
   * Phương thức lấy dữ liệu theo id
   */
  get(id) {
    return BaseAPIConfig.get(`${this.controller}/${id}`);
  }
  /**
   * Hàm lấy dữ liệu phân trang
   * @param {*} payload
   */
  paging(Filter,pageIndex,pageSize) {
    return BaseAPIConfig.get(`${this.controller}/filter?EmployeeFilter=${Filter}&PageIndex=${pageIndex}&PageSize=${pageSize}`); 
  }                                                                   
  /**                                                                 
   * Hàm cập nhật dữ liệu
   * @param {*} id
   * @param {*} body
   */
  put(id, body) {
    return BaseAPIConfig.put(`${this.controller}/${id}`, body);
  }
  /**
   * Hàm cập thêm mới dữ liệu
   * @param {*} id
   * @param {*} body
   */
   post( body) {
    return BaseAPIConfig.post(`${this.controller}`, body);
  }
  /**
   * Hàm xóa bản ghi
   * @param {*} id
   */
  delete(id) {
    return BaseAPIConfig.delete(`${this.controller}/${id}`);
  }

  /**
   * Hàm xóa nhiều bản ghi
   * @param {*} listId
   */
  deleteListId(listId){
    return BaseAPIConfig.post(`${this.controller}/listEntityId`,listId);
  }

  /**
   * Hàm lấy ra đanh sách theo property
   * @param {*} properTy
   */
   getByProperTy(properTy){
    return BaseAPIConfig.get(`${this.controller}/property/${properTy}`);
  }

  /**
   * Hàm export dữ liệu
   */
  exportData(Filter,pageIndex,pageSize){
    return BaseAPIConfig.get(`${this.controller}/Export?EmployeeFilter=${Filter}&PageIndex=${pageIndex}&PageSize=${pageSize}`,{
        responseType: "blob",
      }
  )
  }
}