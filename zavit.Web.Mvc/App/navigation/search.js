export function search() {
    $("a[data-type]").on("click", function(e) {
        e.preventDefault();
        $('#search_concept').text(($(this).attr("data-type")));
        $('#search_input').attr('placeholder', $(this).attr("data-value"));
    });
}

