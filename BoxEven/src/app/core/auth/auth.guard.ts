import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService, 
    private alertaService: AlertaService,
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if(this.authService.usuarioEstaAutenticado()){
      return true;
    }
    else{
      this.authService.encerarSessaoUsuario();
      this.alertaService.erro('Usuário não autenticado');
      return false;
    }
    
  }
  
}
