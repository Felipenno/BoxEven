import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { ResultadoModel } from 'src/app/shared/models/resultado-Model';
import { Usuario } from 'src/app/shared/models/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private  endPointUrl = 'usuario';

  constructor(private comunicacaoService: ComunicacaoService, private authService: AuthService) { }

  login(usuario : Usuario): Observable<ResultadoModel>{
    return this.comunicacaoService.salvar<Usuario, ResultadoModel>(this.endPointUrl + '/login', usuario).pipe();
  }

  registrar(usuario : Usuario): Observable<ResultadoModel>{
    return this.comunicacaoService.salvar<Usuario, ResultadoModel>(this.endPointUrl + '/registrar', usuario).pipe();
  }

}
