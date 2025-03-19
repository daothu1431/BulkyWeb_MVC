

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
            "ajax": { url:'/admin/product/getall'},
            "columns": [
                { data: 'title', "width":"20%" },
                { data: 'isbn', "width": "20%" },
                { data: 'price', "width": "5%" },
                { data: 'author', "width": "15%" },
                { data: 'category.name', "width": "15%" }
                ]
            });
}
