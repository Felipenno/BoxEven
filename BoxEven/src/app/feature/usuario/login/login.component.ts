import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
import { Usuario } from 'src/app/shared/models/usuario';
import { UsuarioService } from '../usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  usuarioForm!: FormGroup;

  constructor(
    private router : Router,
    private usuarioService: UsuarioService,
    private authService: AuthService,
    private alertaService: AlertaService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    if(this.authService.usuarioEstaAutenticado()){
      this.router.navigateByUrl('/sistema');
    }
    this.validacaoForm();
  }

  private validacaoForm(): void{
    this.usuarioForm = this.fb.group({
      apelido: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
      senha: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]],
    })
  }

  submitForm(): void{
    let usuario: Usuario = this.usuarioForm.value;

    this.usuarioService.login(usuario).subscribe({
      next: dados => {
        if(dados && dados.objeto){
          if(dados.sucesso){
            this.authService.definirDadosUsuario(dados.objeto.token);
            this.router.navigateByUrl('/sistema');
          }
          else{
            this.alertaService.erro(dados.mensagem)
          }
          
        }
      }
    })
  }
}
