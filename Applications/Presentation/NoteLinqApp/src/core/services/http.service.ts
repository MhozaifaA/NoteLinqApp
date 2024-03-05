import { OperationResult } from "../helper/OperationResult";
import Storage from "../services/storage.service";
import axios from 'axios';

export function axiosConfig() {
    axios.interceptors.request.use(function (config) {

        config.headers.Authorization = `${'Bearer'} ${Storage.GetAccessToken()}`;

        return config;
    }, function (error) {
        return Promise.reject(error);
    });
}

//use axios as provider by this eazy to controll result in store/ and change provider will not brok
class HttpAppService {


     _Put<T>(url: string, body: any, role?: string | null, action?: (res: T) => void): OperationResult<T> {
        let operation = new OperationResult<T>(this);

         axios.put<T>(
            url, body).then(resp => {
                if (action) {
                    action(resp.data);
                }
                operation.handleResult(resp.data)
            }).catch(err => {
                operation.handleError(err);
            });

        return operation;
    }

     _Post<T>(url: string, body: any, role?: string | null, action?: (res: T) => void, blob: boolean = false): OperationResult<T> {
        let operation = new OperationResult<T>(this);

        let _reqconf;

        if (blob) {
            _reqconf = <any>{
                responseType: 'blob'
            }
        }


         axios.post<T>(
            url, body, _reqconf).then(resp => {
                if (action) {
                    action(resp.data);
                }
                operation.handleResult(resp.data)
            }).catch(err => {

                operation.handleError(err);
            });

        return operation;
    }
    //async promise await

     _Get<T>(url: string, role?: string | null, action?: (res: T) => void, blob: boolean = false): OperationResult<T> {
        let operation = new OperationResult<T>(this);

        let _reqconf;


        if (blob) {
            _reqconf = <any>{
                responseType: 'blob'
            }
        }

         axios.get<T>(
            url, _reqconf).then(resp => {

                if (action) {
                    action(resp.data);
                }
               
                operation.handleResult(resp.data)
            }).catch(err => {

                operation.handleError(err);
            });

        return operation;
    }

     _Delete<T>(url: string, role?: string | null, action?: (res: T) => void): OperationResult<T> {
        let operation = new OperationResult<T>(this);

         axios.delete<T>(
            url).then(resp => {
                if (action) {
                    action(resp.data);
                }
                operation.handleResult(resp.data)
            }).catch(err => {
                operation.handleError(err);
            });

        return operation;
    }

}

const _service = new HttpAppService();
export default _service