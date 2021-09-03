import BaseAPI from "@/api/base/BaseApi.js";

class EmployeesAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "v1/Employees";
  }
}

export default new EmployeesAPI();