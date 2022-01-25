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

//Check if password or email are matching the confirm email/passcode fields
//matchFields(group: FormGroup) {
//  //Check if password and confirmPassword match
//  if (group.contains('confirmPassword')) {
//    let confirmPassword = group.get('confirmPassword');
//    if (confirmPassword.errors == null || 'passwordMismatch' in confirmPassword.errors) {
//      if (group.get('newPassword').value != confirmPassword.value) {
//        confirmPassword.setErrors({ passwordMismatch: true })
//      }
//      else {
//        confirmPassword.setErrors(null);
//      }
//    }
//  }
//  //Check if email and confirmEmail match
//  else if (group.contains('confirmEmail')) {
//    let confirmEmail = group.get('confirmEmail');
//    if (confirmEmail.errors == null || 'emailMismatch' in confirmEmail.errors) {
//      if (group.get('email').value != confirmEmail.value) {
//        confirmEmail.setErrors({ emailMismatch: true })
//      }
//      else {
//        confirmEmail.setErrors(null);
//      }
//    }
//  }

//}
