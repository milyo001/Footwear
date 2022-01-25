import { FormControl } from '@angular/forms';

export function validateOldAndNewPassword(control: FormControl) {
  if (control.parent == null) {
    return null;
  }
  let pass = control.parent.get('password').value;
  let newPass = control.value;
  return pass === newPass ? { passwordsMismatch: true } : null;
}


