import { FormControl } from '@angular/forms';

export function validateOldAndNewPass(control: FormControl) {
  if (control.parent == null) {
    return null;
  }
  let pass = control.parent.get('password').value;
  let newPass = control.value;
  return pass === newPass ? { passwordsMismatch: true } : null;
}

export function validateNewAndConfPass(control: FormControl) {
  if (control.parent == null) {
    return null;
  }
  let newPass = control.parent.get('newPassword').value;
  let confPass = control.value;
  return newPass !== confPass ? { confPasswordMismatch: true } : null;
}

export function valideEmails(control: FormControl) {
  if (control.parent == null) {
    return null;
  }
  let email = control.parent.get('email').value;
  let confEmail = control.value;
  return email !== confEmail ? { emailMismatch: true } : null;
}
