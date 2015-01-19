package org.gradle;

import org.apache.commons.collections.list.GrowthList;

public class Categoria {
    private final String name;

    public Categoria(String name) {
        this.name = name;
        new GrowthList();
    }

    public String getName() {
        return name;
    }
}
