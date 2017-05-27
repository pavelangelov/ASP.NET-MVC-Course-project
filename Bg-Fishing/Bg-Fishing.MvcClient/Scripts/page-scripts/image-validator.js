$(function() {
      $('#file-value').bind('change', function () {
          let fileSize = this.files[0].size,
              maxSize = 3 * 1024 * 1000;

          if (fileSize > maxSize) {
              $('#file-status').addClass('text-danger')
                  .html('Избрания файл надвишава допустимия размер. Ако не го замените с валиден, ще бъде сложена снимката по подразбиране зададена на сървъра!')
          } else {
              $('#file-status').addClass('text-success')
                  .html('Големината на файла е в разрешения максимум.')
          }

      });
  });