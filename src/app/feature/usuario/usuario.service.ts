import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { Usuario } from 'src/app/shared/models/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private  endPointUrl = 'usuario';

  constructor(private comunicacaoService: ComunicacaoService) { }

  login(usuario : Usuario): Observable<Usuario>{
    return this.comunicacaoService.salvar<Usuario>(this.endPointUrl + '/login', usuario).pipe();
  }

  registrar(usuario : Usuario): Observable<Usuario>{
    return this.comunicacaoService.salvar<Usuario>(this.endPointUrl + '/registrar', usuario).pipe();
  }
}
