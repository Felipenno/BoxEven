import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from "@auth0/angular-jwt";
import { DadosToken } from 'src/app/shared/models/dados-token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private nomeUsuario: string = '';
  private idUsuario: string = '';
  private emailUsuario: string = '';
  private apelidoUsuario: string = '';

  jwtHelper = new JwtHelperService();

  constructor(private router: Router) { }

  usuarioEstaAutenticado(): boolean{
    if(this.obterToken()){
      const token = this.obterToken();
      return !this.jwtHelper.isTokenExpired(token);
    }

    return false
  }

  obterNomeUsuario(): string{
    if(this.nomeUsuario){
      return this.nomeUsuario;
    }

    const nome = localStorage.getItem('NOMEUSUARIO');
    if(nome){
      return nome as string;
    }
    
    return '';
  }

  obterEmailUsuario(): string{
    if(this.emailUsuario){
      return this.emailUsuario;
    }

    const nome = localStorage.getItem('EMAILUSUARIO');
    if(nome){
      return nome as string;
    }
    
    return '';
  }

  obterApelidoUsuario(): string{
    if(this.apelidoUsuario){
      return this.apelidoUsuario;
    }

    const nome = localStorage.getItem('APELIDOUSUARIO');
    if(nome){
      return nome as string;
    }
    
    return '';
  }

  obterIdUsuario(): string{
    if(this.idUsuario){
      return this.idUsuario;
    }

    const nome = localStorage.getItem('IDUSUARIO');
    if(nome){
      return nome as string;
    }
    
    return '';
  }

  obterToken(): string {
    const token = localStorage.getItem('TOKENUSUARIO');
    return token ? token : '';
  }

  definirDadosUsuario(token: string): void{
    localStorage.setItem('TOKENUSUARIO', token);

    if(this.obterToken()){
      const token = this.obterToken() as string;
      const dados = this.jwtHelper.decodeToken<DadosToken>(token);
      if(dados){
        this.nomeUsuario = dados.name;
        this.idUsuario = dados.nameid;
        this.emailUsuario = dados.email;
        this.apelidoUsuario = dados.given_name;

        localStorage.setItem('NOMEUSUARIO', dados.name);
        localStorage.setItem('IDUSUARIO', dados.nameid);
        localStorage.setItem('EMAILUSUARIO', dados.email);
        localStorage.setItem('APELIDOUSUARIO', dados.given_name);
      }
    }

  }

  encerarSessaoUsuario(){
    localStorage.removeItem('TOKENUSUARIO');
    localStorage.removeItem('NOMEUSUARIO');
    localStorage.removeItem('IDUSUARIO');
    localStorage.removeItem('EMAILUSUARIO');
    localStorage.removeItem('APELIDOUSUARIO');
    this.router.navigateByUrl('/usuario')
  }
}
