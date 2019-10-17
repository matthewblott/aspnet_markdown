document.addEventListener('DOMContentLoaded', () => {

  document.querySelectorAll('input,textarea,select').forEach((el) =>
    el.addEventListener('invalid', (e) => e.preventDefault(), true));

  const selector1 = 'data-html5-compare';

  document.querySelectorAll(`input[${selector1}]`).forEach((c) => {
    const p = document.getElementById(c.attributes[selector1].value);
    const e = () => c.setCustomValidity(c.value === p.value ? '' : '.');

    p.addEventListener('change', e, true);
    c.addEventListener('keyup', e, true);

  });

  const selector2 = 'data-html5-action';

  document.querySelectorAll(`input[${selector2}]`).forEach((el) => {

    el.addEventListener('keyup', () => {

      const action = el.attributes[selector2].value;

      fetch(`/${action}?${el.id}=${el.value}`).then((resp) => resp.json()).then((j) => {
        el.setCustomValidity(j ? '.' : '');
      });

    }, true);

  });

});