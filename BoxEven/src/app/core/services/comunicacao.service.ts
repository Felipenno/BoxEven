import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ParamsModel } from '../../shared/models/params-model';
import { environment } from 'src/environments/environment';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ComunicacaoService {

  private readonly baseUrl = environment.apiSettings.url;

  constructor(
    private http : HttpClient,
    private alertaService: AlertaService,
    private authService: AuthService
  ) {}

  listar<T>(endPointUrl: string, paramsList?: ParamsModel[]): Observable<T>{
    if(paramsList && paramsList.length > 0){
      let httpParams: HttpParams = new HttpParams();
      
      paramsList?.forEach((value) => {
        if (value.valor || value.valor === false){
          httpParams = httpParams.set(value.nome, value.valor);
        }
      })

      return this.http.get<T>(this.baseUrl + endPointUrl, {params: httpParams}).pipe(x => this.tratarErros(x))
    }
    else{
      return this.http.get<T>(this.baseUrl + endPointUrl).pipe(x => this.tratarErros(x));
    }
  }

  listarPorId<T>(endPointUrl: string, routeid: number): Observable<T>{
    return this.http.get<T>(this.baseUrl + endPointUrl + '/' + routeid).pipe(x => this.tratarErros(x));
  }

  salvar<E, S>(endPointUrl: string, data : E): Observable<S>{
    return this.http.post<S>(this.baseUrl + endPointUrl, data).pipe(x => this.tratarErros(x));
  }

  editar<E, S>(endPointUrl: string, routeid: number, data : E): Observable<S>{
    return this.http.put<S>(this.baseUrl + endPointUrl + '/' + routeid, data).pipe(x => this.tratarErros(x));
  }

  patch<E, S>(endPointUrl: string, data : E): Observable<S>{
    return this.http.patch<S>(this.baseUrl + endPointUrl, data).pipe(x => this.tratarErros(x));
  }

  patchAny(endPointUrl: string, routeid: string, data : any): Observable<any>{
    return this.http.patch<any>(this.baseUrl + endPointUrl + '/' + routeid, data).pipe(x => this.tratarErros(x));
  }

  deletar(endPointUrl: string, routeid: number): Observable<any>{
    return this.http.delete(this.baseUrl + endPointUrl + '/' + routeid).pipe(x => this.tratarErros(x));
  }

  private tratarErros(res: Observable<any>): Observable<any>{
    return res.pipe(catchError(err => {
      ('erro cath')
      if(err.status === 403 || err.status === 401){
        this.alertaService.erro('Sessão expirada');
        setTimeout(() => this.authService.encerarSessaoUsuario(), 2000);
        return res;
      }else if(err.error){
        ('erro cath 2')
        this.alertaService.erro(err.error.mensagem ?? 'Ocorreu um erro interno');
        return res
      }else{
        ('erro cath 3')
        this.alertaService.erro('Ocorreu um erro inesperado');
        return res;
      }
    })) 
  }
}