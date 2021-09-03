import APIConfig from "../config/ApiConfig";
import axios from "axios";

var BaseAPIConfig = axios.create({
  baseURL: APIConfig,
  headers: { "Content-type": "application/json" },
});

export default BaseAPIConfig;