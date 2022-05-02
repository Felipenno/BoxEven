import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ParamsModel } from '../../shared/models/params-model';

@Injectable({
  providedIn: 'root'
})
export class ComunicacaoService {

  private readonly baseUrl = 'https://localhost:7237/api/'

  constructor(private http : HttpClient) {}

  listar<T>(endPointUrl: string, paramsList?: ParamsModel[]): Observable<T>{
    if(paramsList && paramsList.length > 0){
      let httpParams: HttpParams = new HttpParams();
      
      paramsList?.forEach((value) => {
        if (value.valor || value.valor === false){
          httpParams = httpParams.set(value.nome, value.valor)
        }
      })

      return this.http.get<T>(this.baseUrl + endPointUrl, {params: httpParams})
    }
    else{
      return this.http.get<T>(this.baseUrl + endPointUrl);
    }
  }

  listarPorId<T>(endPointUrl: string, routeid: number): Observable<T>{
    return this.http.get<T>(this.baseUrl + endPointUrl + '/' + routeid);
  }

  salvar<T>(endPointUrl: string, data : T): Observable<T>{
    return this.http.post<T>(this.baseUrl + endPointUrl, data);
  }

  editar<T>(endPointUrl: string, routeid: number, data : T): Observable<T>{
    return this.http.put<T>(this.baseUrl + endPointUrl + '/' + routeid, data);
  }

  patch<T>(endPointUrl: string, data : T): Observable<T>{
    return this.http.patch<T>(this.baseUrl + endPointUrl, data);
  }

  patchAny(endPointUrl: string, routeid: string, data : any): Observable<any>{
    return this.http.patch<any>(this.baseUrl + endPointUrl + '/' + routeid, data);
  }

  deletar(endPointUrl: string, routeid: number): Observable<any>{
    return this.http.delete(this.baseUrl + endPointUrl + '/' + routeid);
  }
}